using System;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.AreaFilters;
using DronZone_UWP.Presentation.Views.Area;
using DronZone_UWP.Presentation.Views.AreaFilter;
using DronZone_UWP.Utils;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.AreaFilters
{
    public class FilterDetailsViewModel : ViewModelBase
    {
        private readonly IAreaFilterService _areaFilterService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private readonly int _filterId;

        private AreaFilterDetailedModel _filterDetailedModel;

        public FilterDetailsViewModel(IAreaFilterService areaFilterService, MenuNavigationHelper menuNavigationHelper)
        {
            _areaFilterService = areaFilterService;
            _menuNavigationHelper = menuNavigationHelper;

            _filterId = Convert.ToInt32(MenuContentViewModel.Param);

            GoBackToFilterListCommand = ReactiveCommand.CreateFromTask(async () => GoToFilterListExecuted());
            GoBackToAreaDetailsCommand = ReactiveCommand.CreateFromTask(async () => GoBackToAreaDetailsExecuted());

            Init();
        }

        private void GoToFilterListExecuted()
        {
            _menuNavigationHelper.NavigateTo(typeof(AreaFilterListPage), FilterModel.AreaId);
        }

        private void GoBackToAreaDetailsExecuted()
        {
            _menuNavigationHelper.NavigateTo(typeof(AreaDetailsPage), FilterModel.AreaId);
        }

        public AreaFilterDetailedModel FilterModel
        {
            get => _filterDetailedModel;
            set => this.RaiseAndSetIfChanged(ref _filterDetailedModel, value);
        }
        
        public ReactiveCommand GoBackToFilterListCommand { get; }

        public ReactiveCommand GoBackToAreaDetailsCommand { get; }

        private async void Init()
        {
            await LoadFilterDetailsAsync();
        }

        private async Task LoadFilterDetailsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                FilterModel = await _areaFilterService.GetFilterAsync(_filterId);
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }
            finally
            {
                OnIsInProgressChanges(false);
            }
        }
    }
}