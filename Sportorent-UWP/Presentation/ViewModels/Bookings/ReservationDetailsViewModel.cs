using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using DronZone_UWP.Enums;
using DronZone_UWP.Models.Reservation;
using ReactiveUI;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Presentation.ViewModels.Bookings
{
    public class ReservationDetailsViewModel : ViewModelBase
    {
        private readonly IPreferencesService _preferencesService;

        private ReactiveCommand _payCommand;
        private ReactiveCommand _comeInToFieldCommand;
        private ReactiveCommand _comeOutFromFieldCommand;

        private ReservationDetailedModel _reservationDetailedModel;
        private ReservationParticipantModel _reservationParticipantModel;
        private ReservationBillModel _reservationBill;
        private bool _canComeIn;
        private bool _canComeOut;
        private bool _canPayBill;

        public ReservationDetailsViewModel(IPreferencesService preferencesService)
        {
            _preferencesService = preferencesService;

            PayCommand = ReactiveCommand.CreateFromTask(PayExecutedAsync);
            ComeInToFieldCommand = ReactiveCommand.CreateFromTask(ComeIntoExecutedAsync);
            ComeOutFromFieldCommand = ReactiveCommand.CreateFromTask(ComeOutExecutedAsync);

            Init();
        }

        public ReservationDetailedModel Reservation
        {
            get => _reservationDetailedModel;
            set => this.RaiseAndSetIfChanged(ref _reservationDetailedModel, value);
        }

        public ReservationParticipantModel CurrentReservationParticipant
        {
            get => _reservationParticipantModel;
            set => this.RaiseAndSetIfChanged(ref _reservationParticipantModel, value);
        }

        public ReservationBillModel ReservationBill
        {
            get => _reservationBill;
            set => this.RaiseAndSetIfChanged(ref _reservationBill, value);
        }

        public bool CanComeIn
        {
            get => _canComeIn;
            set => this.RaiseAndSetIfChanged(ref _canComeIn, value);
        }

        public bool CanComeOut
        {
            get => _canComeOut;
            set => this.RaiseAndSetIfChanged(ref _canComeOut, value);
        }

        public bool CanPayBill
        {
            get => _canPayBill;
            set => this.RaiseAndSetIfChanged(ref _canPayBill, value);
        }

        public ReactiveCommand PayCommand
        {
            get => _payCommand;
            set => this.RaiseAndSetIfChanged(ref _payCommand, value);
        }

        public ReactiveCommand ComeInToFieldCommand
        {
            get => _comeInToFieldCommand;
            set => this.RaiseAndSetIfChanged(ref _comeInToFieldCommand, value);
        }

        public ReactiveCommand ComeOutFromFieldCommand
        {
            get => _comeOutFromFieldCommand;
            set => this.RaiseAndSetIfChanged(ref _comeOutFromFieldCommand, value);
        }

        private async void Init()
        {
            await LoadReservationDetailsAsync();
        }

        private async Task LoadReservationDetailsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var reservationListItem = MenuContentViewModel.Param as ReservationListItemModel;
                //Reservation = await _reservationsService.GetDetailedReservationAsync(reservationListItem.Id);

                //var userInfo = _preferencesService.UserInfo;
                ////CurrentReservationParticipant = Reservation
                ////    .ReservationParticipants
                ////    .SingleOrDefault(x => x.ParticipantId == userInfo.ParticipantId);
                //ReservationBill = CurrentReservationParticipant?.ReservationBill;
                //CanComeIn = ReservationBill?.Status == BillStatuses.Paid
                //            && CurrentReservationParticipant.Status != ReservationParticipantStatuses.Entered
                //            && CurrentReservationParticipant.Status != ReservationParticipantStatuses.Finished
                //            && Reservation.ReservationStartsAt <= DateTime.UtcNow.AddHours(2)
                //            && Reservation.ReservationEndsAt > DateTime.UtcNow.AddHours(2);
                //CanComeOut = ReservationBill?.Status == BillStatuses.Paid
                //            && CurrentReservationParticipant.Status == ReservationParticipantStatuses.Entered
                //            && Reservation.ReservationStartsAt <= DateTime.UtcNow.AddHours(2);
                //CanPayBill = ReservationBill?.Status == BillStatuses.NotPaid;
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

        private async Task PayExecutedAsync()
        {
            var cd = new ContentDialog();
            cd.Title = "Pay";
            cd.PrimaryButtonText = "Pay";
            cd.PrimaryButtonCommand = ReactiveCommand.CreateFromTask(async _ =>
            {
                OnIsInProgressChanges(true);

                try
                {
                    //await _billingService.PayBill(ReservationBill.Id);
                    await LoadReservationDetailsAsync();
                }
                catch (Exception ex)
                {
                    // ignored
                }
                finally
                {
                    OnIsInProgressChanges(false);
                    cd.Hide();
                }
            });
            cd.CloseButtonText = "Cancel";

            await cd.ShowAsync();
        }

        private async Task ComeIntoExecutedAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var reservationId = CurrentReservationParticipant.ReservationId;
                var participantId = CurrentReservationParticipant.ParticipantId;

                //await _reservationsService.GetIntoFieldAsync(reservationId, participantId);
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

        private async Task ComeOutExecutedAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var reservationId = CurrentReservationParticipant.ReservationId;
                var participantId = CurrentReservationParticipant.ParticipantId;

                //await _reservationsService.GetOutOfFieldAsync(reservationId, participantId);
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