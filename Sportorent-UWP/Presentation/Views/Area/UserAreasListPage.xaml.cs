using System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Area;
using DronZone_UWP.Presentation.ViewModels.Drone;
using ReactiveUI;

namespace DronZone_UWP.Presentation.Views.Area
{
    public sealed partial class UserAreasListPage : IViewFor<UserAreasListViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(UserAreasListViewModel),
                typeof(UserAreasListPage),
                new PropertyMetadata(default(UserAreasListViewModel)));

        public UserAreasListPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<UserAreasListViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.AreaList, v => v.AreaListView.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedArea, v => v.AreaListView.SelectedItem));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (UserAreasListViewModel)value;
        }

        public UserAreasListViewModel ViewModel
        {
            get => (UserAreasListViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
