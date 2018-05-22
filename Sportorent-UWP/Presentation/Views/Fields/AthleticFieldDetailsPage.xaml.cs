using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.AthleticField;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.Fields
{
    public sealed partial class AthleticFieldDetailsPage : IViewFor<AthleticFieldDetailsViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                typeof(AthleticFieldDetailsViewModel),
                typeof(AthleticFieldDetailsPage),
                new PropertyMetadata(default(AthleticFieldDetailsViewModel)));

        public AthleticFieldDetailsPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<AthleticFieldDetailsViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.AthleticFieldModel.Name, v => v.FieldNameTextBlock.Text));

            d(this.OneWayBind(ViewModel, vm => vm.AthleticFieldModel.Name, v => v.FieldNameTextBlock.Text));
            d(this.OneWayBind(ViewModel, vm => vm.AthleticFieldModel.Status, v => v.StatusTextBlock.Text));
            d(this.OneWayBind(ViewModel, vm => vm.AthleticFieldModel.WorkingHours, v => v.WorkingHoursTextBlock.Text));
            //d(this.OneWayBind(ViewModel, vm => vm.AthleticFieldModel.Position, v => v.PositionTextBlock.Text));
            d(this.OneWayBind(ViewModel, vm => vm.AthleticFieldModel.PricePerParticipant, v => v.HourPriceTextBlock.Text));

            d(this.BindCommand(ViewModel, vm => vm.GoToAddReservationPageCommand, v => v.AddReservationButton));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AthleticFieldDetailsViewModel)value;
        }

        public AthleticFieldDetailsViewModel ViewModel
        {
            get => (AthleticFieldDetailsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
