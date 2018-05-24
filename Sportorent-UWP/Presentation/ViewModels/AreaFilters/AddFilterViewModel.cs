using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Enums;
using DronZone_UWP.Models.AreaFilters;
using DronZone_UWP.Presentation.Views.AreaFilter;
using DronZone_UWP.Utils;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.AreaFilters
{
    public class AddFilterViewModel : ViewModelBase
    {
        private readonly IAreaFilterService _areaFilterService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private readonly string _areaId;

        private AddFilterDetailedModel _addFilterModel;

        public AddFilterViewModel(IAreaFilterService areaFilterService, MenuNavigationHelper menuNavigationHelper)
        {
            _areaFilterService = areaFilterService;
            _menuNavigationHelper = menuNavigationHelper;

            _areaId = MenuContentViewModel.Param as string;

            GoToFiltersCommand = ReactiveCommand.CreateFromTask(async () => GoToFiltersExecuted());
            SaveFilterCommand = ReactiveCommand.CreateFromTask(SaveFilterExecutedAsync);

            AddFilterModel = new AddFilterDetailedModel()
            {
                AreaId = _areaId
            };
        }

        private void GoToFiltersExecuted()
        {
            _menuNavigationHelper.NavigateTo(typeof(AreaFilterListPage), _areaId);
        }

        private async Task SaveFilterExecutedAsync(CancellationToken ct)
        {
            if (AddFilterModel.MaxAvailableWeigth <= 0 ||
                AddFilterModel.MaxDroneSpeed <= 0 ||
                AddFilterModel.MaxDroneWeigth <= 0)
            {
                await ShowErrorAsync("Validation error. Fill all fields with correct values!");
                return;
            }

            await _areaFilterService.CreateFilterAsync(AddFilterModel);
            GoToFiltersExecuted();
        }

        public IList<string> DroneTypeValuesList { get; } = Enum.GetNames(typeof(DroneType)).ToList();

        public AddFilterDetailedModel AddFilterModel
        {
            get => _addFilterModel;
            set => this.RaiseAndSetIfChanged(ref _addFilterModel, value);
        }

        public ReactiveCommand GoToFiltersCommand { get; }

        public ReactiveCommand SaveFilterCommand { get; }
    }
}