using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Bookings;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.Reservation
{
    public sealed partial class AddReservationPage : IViewFor<AddReservationViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                typeof(AddReservationViewModel),
                typeof(AddReservationPage),
                new PropertyMetadata(default(AddReservationViewModel)));

        public AddReservationPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<AddReservationViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            GameDateCalendarPicker.MinDate = DateTimeOffset.Now;

            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.FieldListItem.Name, v => v.FieldNameTextBlock.Text));

            d(this.OneWayBind(ViewModel, vm => vm.FieldReservationList, v => v.ReservedTimeItemsControl.ItemsSource));

            d(this.Bind(ViewModel, vm => vm.Date, v => v.GameDateCalendarPicker.Date));

            d(this.Bind(ViewModel, vm => vm.StartTime, v => v.GameStartsTimePicker.Time));
            d(this.Bind(ViewModel, vm => vm.EndTime, v => v.GameEndsTimePicker.Time));

            d(this.OneWayBind(ViewModel, vm => vm.StartTimeErrorMessage, v => v.GameStartsErrorTextBlock.Text));
            d(this.OneWayBind(ViewModel, vm => vm.StartTimeErrorMessage, v => v.GameStartsErrorTextBlock.Visibility, 
                msg => !string.IsNullOrEmpty(msg) ? Visibility.Visible : Visibility.Collapsed));
            
            d(this.OneWayBind(ViewModel, vm => vm.EndTimeErrorMessage, v => v.GameEndsErrorTextBlock.Text));
            d(this.OneWayBind(ViewModel, vm => vm.EndTimeErrorMessage, v => v.GameEndsErrorTextBlock.Visibility,
                msg => !string.IsNullOrEmpty(msg) ? Visibility.Visible : Visibility.Collapsed));

            d(this.BindCommand(ViewModel, vm => vm.AddReservationCommand, v => v.AddReservationButton));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AddReservationViewModel)value;
        }

        public AddReservationViewModel ViewModel
        {
            get => (AddReservationViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
