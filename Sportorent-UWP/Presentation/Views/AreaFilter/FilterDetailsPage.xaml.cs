using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.AreaFilters;
using ReactiveUI;

namespace DronZone_UWP.Presentation.Views.AreaFilter
{
    public sealed partial class FilterDetailsPage : IViewFor<FilterDetailsViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(FilterDetailsViewModel),
                typeof(FilterDetailsPage),
                new PropertyMetadata(default(FilterDetailsViewModel)));

        public FilterDetailsPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<FilterDetailsViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.FilterModel.DroneTypePresentation, v => v.DroneTypeTextBlock.Text));
            d(this.OneWayBind(ViewModel, vm => vm.FilterModel.MaxAvailableWeigth, v => v.MaxAvailableWeigthTextBlock.Text));
            d(this.OneWayBind(ViewModel, vm => vm.FilterModel.MaxDroneWeigth, v => v.MaxDroneWeigthTextBlock.Text));
            d(this.OneWayBind(ViewModel, vm => vm.FilterModel.MaxDroneSpeed, v => v.MaxDroneSpeedTextBlock.Text));

            d(this.BindCommand(ViewModel, vm => vm.GoBackToAreaDetailsCommand, v => v.GoBackToAreaDetailsButton));
            d(this.BindCommand(ViewModel, vm => vm.GoBackToFilterListCommand, v => v.GoToFilterListButton));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (FilterDetailsViewModel)value;
        }

        public FilterDetailsViewModel ViewModel
        {
            get => (FilterDetailsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
