using System;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.Area;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.Drone
{
    public class DroneDetailsViewModel : ViewModelBase
    {
        private readonly IDroneService _droneService;
        
        private AreaDetailedModel _droneModel;

        public DroneDetailsViewModel(IDroneService droneService)
        {
            _droneService = droneService;
            
            Init();
        }

        public AreaDetailedModel DroneModel
        {
            get => _droneModel;
            set => this.RaiseAndSetIfChanged(ref _droneModel, value);
        }

        private async void Init()
        {
            await LoadDroneDetailsAsync();
        }

        private async Task LoadDroneDetailsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                DroneModel = MenuContentViewModel.Param as AreaDetailedModel;
                //Area = await _droneService.GetDetailedDroneAsync(reservationListItem.Id);
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