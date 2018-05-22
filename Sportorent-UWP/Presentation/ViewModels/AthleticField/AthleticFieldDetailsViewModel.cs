using System;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.AthleticField;
using DronZone_UWP.Presentation.Views.Reservation;
using DronZone_UWP.Utils;
using ReactiveUI;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Presentation.ViewModels.AthleticField
{
    public class AthleticFieldDetailsViewModel : ViewModelBase
    {
        private readonly IDroneService _athleticFieldsService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private ReactiveCommand _goToAddReservationPageCommand;

        private AthleticFieldDetailedModel _athleticFieldModel;

        public AthleticFieldDetailsViewModel(
            IDroneService athleticFieldsService,
            MenuNavigationHelper menuNavigationHelper)
        {
            _athleticFieldsService = athleticFieldsService;
            _menuNavigationHelper = menuNavigationHelper;

            GoToAddReservationPageCommand = ReactiveCommand.Create(GoToAddReservationPageExecuted);

            Init();
        }

        public AthleticFieldDetailedModel AthleticFieldModel
        {
            get => _athleticFieldModel;
            set => this.RaiseAndSetIfChanged(ref _athleticFieldModel, value);
        }

        public ReactiveCommand GoToAddReservationPageCommand
        {
            get => _goToAddReservationPageCommand;
            set => this.RaiseAndSetIfChanged(ref _goToAddReservationPageCommand, value);
        }

        private async void Init()
        {
            await LoadAthleticFieldDetailsAsync();
        }

        private async Task LoadAthleticFieldDetailsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var athleticFieldListItem = MenuContentViewModel.Param as AthleticFieldListItemModel;
                //AthleticFieldModel =
                //    await _athleticFieldsService.GetDetailedAthleticFieldAsync(athleticFieldListItem.Id);
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

        private void GoToAddReservationPageExecuted()
        {
            var athleticFieldListItem = MenuContentViewModel.Param as AthleticFieldListItemModel;
            _menuNavigationHelper.NavigateTo(typeof(AddReservationPage), athleticFieldListItem);
        }
    }
}