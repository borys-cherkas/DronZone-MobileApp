using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Drone;
using ReactiveUI;

namespace DronZone_UWP.Presentation.Views.Drones
{
    public sealed partial class UserDronesListPage : IViewFor<UserDroneListViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(UserDroneListViewModel),
                typeof(UserDronesListPage),
                new PropertyMetadata(default(UserDroneListViewModel)));

        public UserDronesListPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<UserDroneListViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.DroneList, v => v.DroneListView.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedDrone, v => v.DroneListView.SelectedItem));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (UserDroneListViewModel)value;
        }

        public UserDroneListViewModel ViewModel
        {
            get => (UserDroneListViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
