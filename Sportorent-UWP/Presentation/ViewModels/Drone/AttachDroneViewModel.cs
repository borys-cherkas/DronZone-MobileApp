using System;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Data.Api;
using DronZone_UWP.Presentation.Views.Drones;
using DronZone_UWP.Utils;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.Drone
{
    public class AttachDroneViewModel : ViewModelBase
    {
        private readonly IDroneService _droneService;
        private readonly MenuNavigationHelper _menuNavigationHelper;
        
        private string _code;

        public AttachDroneViewModel(IDroneService droneService, MenuNavigationHelper menuNavigationHelper)
        {
            _droneService = droneService;
            _menuNavigationHelper = menuNavigationHelper;

            Code = string.Empty;
            AttachDroneCommand = ReactiveCommand.CreateFromTask(AttachDroneAsync);
        }

        public ReactiveCommand AttachDroneCommand { get; set; }

        public string Code
        {
            get => _code;
            set => this.RaiseAndSetIfChanged(ref _code, value);
        }

        private async Task AttachDroneAsync()
        {
            if (string.IsNullOrEmpty(Code))
            {
                await ShowErrorAsync("Validation Error. Fill all fields with correct values.");
                return;
            }

            OnIsInProgressChanges(true);

            try
            {
                await _droneService.AttachDroneAsync(Code);
                _menuNavigationHelper.NavigateTo(typeof(UserDronesListPage));
            }
            catch (ApiException ex)
            {
                await ShowErrorAsync("Incorrect drone code. Enter valid one.");
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