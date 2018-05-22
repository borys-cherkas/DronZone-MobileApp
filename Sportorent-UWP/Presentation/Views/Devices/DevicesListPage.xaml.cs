using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Devices;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.Devices
{
    public sealed partial class DevicesListPage : IViewFor<DevicesListViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(DevicesListViewModel),
                typeof(DevicesListPage),
                new PropertyMetadata(default(DevicesListViewModel)));

        public DevicesListPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<DevicesListViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.DeviceList, v => v.DevicesListView.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedDevice, v => v.DevicesListView.SelectedItem));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DevicesListViewModel)value;
        }

        public DevicesListViewModel ViewModel
        {
            get => (DevicesListViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
