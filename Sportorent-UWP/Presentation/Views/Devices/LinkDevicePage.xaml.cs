using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Devices;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.Devices
{
    public sealed partial class LinkDevicePage : IViewFor<LinkDeviceViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                typeof(LinkDeviceViewModel),
                typeof(LinkDevicePage),
                new PropertyMetadata(default(LinkDeviceViewModel)));

        public LinkDevicePage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<LinkDeviceViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.Sports, v => v.KindsOfSportComboBox.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedSport, v => v.KindsOfSportComboBox.SelectedItem));

            d(this.OneWayBind(ViewModel, vm => vm.Fields, v => v.FieldComboBox.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedField, v => v.FieldComboBox.SelectedItem));
            d(this.OneWayBind(ViewModel, vm => vm.FieldError, v => v.FieldComboBoxError.Text));
            d(this.OneWayBind(ViewModel, vm => vm.FieldError, v => v.FieldComboBoxError.Visibility,
                msg => !string.IsNullOrEmpty(msg) ? Visibility.Visible : Visibility.Collapsed));

            d(this.Bind(ViewModel, vm => vm.Devices, v => v.DeviceIdComboBox.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedDeviceId, v => v.DeviceIdComboBox.SelectedItem));
            d(this.OneWayBind(ViewModel, vm => vm.DeviceIdError, v => v.DeviceIdError.Text));
            d(this.OneWayBind(ViewModel, vm => vm.DeviceIdError, v => v.DeviceIdError.Visibility,
                msg => !string.IsNullOrEmpty(msg) ? Visibility.Visible : Visibility.Collapsed));

            d(this.BindCommand(ViewModel, vm => vm.SaveCommand, v => v.SubmitButton));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (LinkDeviceViewModel)value;
        }

        public LinkDeviceViewModel ViewModel
        {
            get => (LinkDeviceViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
