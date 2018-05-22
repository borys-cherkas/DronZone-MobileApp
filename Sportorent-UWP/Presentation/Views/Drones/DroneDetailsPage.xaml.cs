using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Drone;
using ReactiveUI;

namespace DronZone_UWP.Presentation.Views.Drones
{
    public sealed partial class DroneDetailsPage : IViewFor<DroneDetailsViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(DroneDetailsViewModel),
                typeof(DroneDetailsPage),
                new PropertyMetadata(default(DroneDetailsViewModel)));

        public DroneDetailsPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<DroneDetailsViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.DroneModel.Name, v => v.DroneNameTextBlock.Text));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DroneDetailsViewModel)value;
        }

        public DroneDetailsViewModel ViewModel
        {
            get => (DroneDetailsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
