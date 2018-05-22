using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Area;
using ReactiveUI;

namespace DronZone_UWP.Presentation.Views.Area
{
    public sealed partial class AreaDetailsPage : IViewFor<AreaDetailsViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(AreaDetailsViewModel),
                typeof(AreaDetailsPage),
                new PropertyMetadata(default(AreaDetailsViewModel)));

        public AreaDetailsPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<AreaDetailsViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.Area.Name, v => v.AreaNameTextBlock.Text));

            //d(this.BindCommand(ViewModel, vm => vm.PayCommand, v => v.PayButton));
            //d(this.OneWayBind(ViewModel, vm => vm.CanPayBill, v => v.PayButton.Visibility,
            //    canPay => canPay ? Visibility.Visible : Visibility.Collapsed));

            //d(this.BindCommand(ViewModel, vm => vm.ComeInToFieldCommand, v => v.ComeInButton));
            //d(this.OneWayBind(ViewModel, vm => vm.CanComeIn, v => v.ComeInButton.Visibility,
            //    canCome => canCome ? Visibility.Visible : Visibility.Collapsed));

            //d(this.BindCommand(ViewModel, vm => vm.ComeOutFromFieldCommand, v => v.ComeOutButton));
            //d(this.OneWayBind(ViewModel, vm => vm.CanComeOut, v => v.ComeOutButton.Visibility,
            //    canCome => canCome ? Visibility.Visible : Visibility.Collapsed));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AreaDetailsViewModel)value;
        }

        public AreaDetailsViewModel ViewModel
        {
            get => (AreaDetailsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
