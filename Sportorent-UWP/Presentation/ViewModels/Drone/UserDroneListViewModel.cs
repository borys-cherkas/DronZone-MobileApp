using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.Drone;
using DronZone_UWP.Utils;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.Drone
{
    public class UserDroneListViewModel : ViewModelBase
    {
        private readonly IDroneService _droneService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private ReactiveList<DroneDetailedModel> _droneList;
        private DroneDetailedModel _selectedDrone;

        public UserDroneListViewModel(IDroneService droneService, MenuNavigationHelper menuNavigationHelper)
        {
            _droneService = droneService;
            _menuNavigationHelper = menuNavigationHelper;

            DroneList = new ReactiveList<DroneDetailedModel>();

            this.ObservableForProperty(x => x.SelectedDrone)
                .Where(x => x.Value != null)
                .Subscribe(args =>
                {
                    GoToDroneDetails();
                });

            Init();
        }

        public ReactiveList<DroneDetailedModel> DroneList
        {
            get => _droneList;
            set => this.RaiseAndSetIfChanged(ref _droneList, value);
        }

        public DroneDetailedModel SelectedDrone
        {
            get => _selectedDrone;
            set => this.RaiseAndSetIfChanged(ref _selectedDrone, value);
        }
        
        private async void Init()
        {
            await LoadDronesAsync();
        }

        private void GoToDroneDetails()
        {
            //_menuNavigationHelper.NavigateTo(typeof(DroneDetailsPage), SelectedDrone);
        }

        private async Task LoadDronesAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                // WHY???
                if (_droneService == null) return;

                var drones = await _droneService.GetUserDronesAsync();

                DroneList.Clear();
                DroneList.AddRange(drones);
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
