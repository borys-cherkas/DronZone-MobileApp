using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.AthleticField;
using DronZone_UWP.Models.Reservation;
using DronZone_UWP.Presentation.Views.Reservation;
using DronZone_UWP.Utils;
using ReactiveUI;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Presentation.ViewModels.Bookings
{
    public class AddReservationViewModel : ViewModelBase
    {
        private readonly IAreaService _reservationsService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private ReactiveList<ReservationListItemModel> _fieldReservationList;
        private ReactiveCommand _addReservationCommand;
        private DateTimeOffset? _date;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private string _startTimeErrorMessage;
        private string _endTimeErrorMessage;

        public AddReservationViewModel(IAreaService reservationsService, MenuNavigationHelper menuNavigationHelper)
        {
            _reservationsService = reservationsService;
            _menuNavigationHelper = menuNavigationHelper;

            FieldListItem = MenuContentViewModel.Param as AthleticFieldListItemModel;
            Init();

            AddReservationCommand = ReactiveCommand.CreateFromTask(AddReservationExecuted);
        }

        private async void Init()
        {
            StartTimeErrorMessage = EndTimeErrorMessage = "";
            FieldReservationList = new ReactiveList<ReservationListItemModel>();
            Date = DateTime.Today;

            OnIsInProgressChanges(true);

            await LoadReservedTimeForFieldAsync();

            this.ObservableForProperty(x => x.Date)
                .Subscribe(x =>
                {
                    ValidateStartEndDateTime();
                });

            this.ObservableForProperty(x => x.StartTime)
                .Merge(this.ObservableForProperty(vm => vm.EndTime))
                .Subscribe(x =>
                {
                    ValidateStartEndDateTime();
                });

            OnIsInProgressChanges(false);
        }

        private async Task LoadReservedTimeForFieldAsync()
        {
            //var reservationList = await _reservationsService.GetFieldReservationsAsync(FieldListItem.Id);
            //FieldReservationList = new ReactiveList<ReservationListItemModel>(reservationList.ReservationList.SortReservations().ToList());
        }

        private bool ValidateStartEndDateTime()
        {
            if (!Date.HasValue)
            {
                return false;
            }

            StartTimeErrorMessage = EndTimeErrorMessage = "";

            var filteredByDate = FieldReservationList.Where(x => x.ReservationDate.Date == Date.Value.Date);
            bool isValid = StartTime < EndTime;
            if (!isValid)
            {
                StartTimeErrorMessage = "Start time cannot be later than end time!";
                EndTimeErrorMessage = "End time cannot be earlier than start time!";
                return false;
            }

            isValid = StartTime >= FieldListItem.StartsWorkingAt.TimeOfDay 
                && EndTime <= FieldListItem.EndsWorkingAt.TimeOfDay;
            if (!isValid)
            {
                var workingHours = $"{FieldListItem.StartsWorkingTime} - {FieldListItem.EndsWorkingTime}";

                StartTimeErrorMessage = "Please, chose time between working hours: " + workingHours;
                EndTimeErrorMessage = "Please, chose time between working hours: " + workingHours;
                return false;
            }

            isValid = filteredByDate.All(x =>
                x.ReservationEndsAt.TimeOfDay <= StartTime
                || x.ReservationStartsAt.TimeOfDay >= EndTime);
            if (!isValid)
            {
                string errorMessage = "This time is already booked! Please, chose another.";
                StartTimeErrorMessage = errorMessage;
                EndTimeErrorMessage = errorMessage;
                return false;
            }

            return true;
        }

        public DateTimeOffset? Date
        {
            get { return _date; }
            set { this.RaiseAndSetIfChanged(ref _date, value); }
        }

        public TimeSpan StartTime
        {
            get { return _startTime; }
            set { this.RaiseAndSetIfChanged(ref _startTime, value); }
        }

        public TimeSpan EndTime
        {
            get { return _endTime; }
            set { this.RaiseAndSetIfChanged(ref _endTime, value); }
        }

        public string StartTimeErrorMessage
        {
            get { return _startTimeErrorMessage; }
            set { this.RaiseAndSetIfChanged(ref _startTimeErrorMessage, value); }
        }

        public string EndTimeErrorMessage
        {
            get { return _endTimeErrorMessage; }
            set { this.RaiseAndSetIfChanged(ref _endTimeErrorMessage, value); }
        }

        public ReactiveCommand AddReservationCommand
        {
            get { return _addReservationCommand; }
            private set { this.RaiseAndSetIfChanged(ref _addReservationCommand, value); }
        }

        public AthleticFieldListItemModel FieldListItem { get; set; }

        public ReactiveList<ReservationListItemModel> FieldReservationList
        {
            get { return _fieldReservationList; }
            private set { this.RaiseAndSetIfChanged(ref _fieldReservationList, value); }
        }

        //public ReactiveList<ReservationListItemModel> FieldTodayReservationList =>
        //    new ReactiveList<ReservationListItemModel>(FieldReservationList.Where(x => x.ReservationDate.Date == DateTime.Today).ToList());?

        private async Task AddReservationExecuted()
        {
            if (!ValidateStartEndDateTime())
            {
                return;
            }

            OnIsInProgressChanges(true);

            var model = new AddReservationModel()
            {
                AthleticFieldId = FieldListItem.Id,
                StartsAt = Date.Value.Date.Add(StartTime),
                EndsAt = Date.Value.Date.Add(EndTime)
            };

            try
            {
                //bool res = await _reservationsService.AddReservationAsync(model);
                //if (res)
                //{
                //    await ShowMessageAsync("Reservation was added successfully.");
                //    _menuNavigationHelper.NavigateTo(typeof(UserAreasListViewModel));
                //}
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
