using System;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.Area;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.Area
{
    public class AreaDetailsViewModel : ViewModelBase
    {
        private readonly IAreaService _areaService;

        private ReactiveCommand _goToFiltersCommand;

        private AreaDetailedModel _areaDetailedModel;

        public AreaDetailsViewModel(IAreaService areaService)
        {
            _areaService = areaService;

            GoToFiltersCommand = ReactiveCommand.CreateFromTask(GoToFiltersExecutedAsync);

            Init();
        }

        private Task GoToFiltersExecutedAsync()
        {
            return Task.CompletedTask;
        }

        public AreaDetailedModel Area
        {
            get => _areaDetailedModel;
            set => this.RaiseAndSetIfChanged(ref _areaDetailedModel, value);
        }

        public ReactiveCommand GoToFiltersCommand
        {
            get => _goToFiltersCommand;
            set => this.RaiseAndSetIfChanged(ref _goToFiltersCommand, value);
        }

        private async void Init()
        {
            await LoadAreaDetailsAsync();
        }

        private async Task LoadAreaDetailsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                Area = MenuContentViewModel.Param as AreaDetailedModel;
                //Area = await _areaService.GetDetailedAreaAsync(reservationListItem.Id);
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