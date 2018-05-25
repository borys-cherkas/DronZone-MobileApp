using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Drone;
using ReactiveUI;

namespace DronZone_UWP.Presentation.Views.Drones
{
    public sealed partial class AttachDronePage : IViewFor<AttachDroneViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(AttachDroneViewModel),
                typeof(AttachDronePage),
                new PropertyMetadata(default(AttachDroneViewModel)));

        public AttachDronePage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<AttachDroneViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.Bind(ViewModel, vm => vm.Code, v => v.DroneCodeTextBox.Text));
            d(this.BindCommand(ViewModel, vm => vm.AttachDroneCommand, v => v.AttachDroneButton));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AttachDroneViewModel)value;
        }

        public AttachDroneViewModel ViewModel
        {
            get => (AttachDroneViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
