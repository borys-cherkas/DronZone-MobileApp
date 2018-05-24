using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Autofac;
using DronZone_UWP.Enums;
using DronZone_UWP.Presentation.ViewModels.AreaFilters;
using ReactiveUI;

namespace DronZone_UWP.Presentation.Views.AreaFilter
{
    public sealed partial class AddFilterPage : IViewFor<AddFilterViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(AddFilterViewModel),
                typeof(AddFilterPage),
                new PropertyMetadata(default(AddFilterViewModel)));

        public AddFilterPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<AddFilterViewModel>();
            

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.BindCommand(ViewModel, vm => vm.GoToFiltersCommand, v => v.GoBackToFiltersButton));
            d(this.BindCommand(ViewModel, vm => vm.SaveFilterCommand, v => v.SaveFilterButton));

            d(this.OneWayBind(ViewModel, vm => vm.DroneTypeValuesList, v => v.DroneTypeComboBox.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.AddFilterModel.DroneType, v => v.DroneTypeComboBox.SelectedIndex));
            d(this.Bind(ViewModel, vm => vm.AddFilterModel.MaxAvailableWeigth, v => v.MaxWeigthTextBox.Text, DoubleToStringFunc, StringToDoubleFunc));
            d(this.Bind(ViewModel, vm => vm.AddFilterModel.MaxDroneSpeed, v => v.MaxSpeedTextBox.Text, DoubleToStringFunc, StringToDoubleFunc));
            d(this.Bind(ViewModel, vm => vm.AddFilterModel.MaxDroneWeigth, v => v.MaxCarryingCapacityTextBox.Text, DoubleToStringFunc, StringToDoubleFunc));
        }

        private string DoubleToStringFunc(double number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }

        private double StringToDoubleFunc(string value)
        {
            var isSuccess = double.TryParse(value, out var result);
            return isSuccess ? result : -1d;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AddFilterViewModel) value;
        }

        public AddFilterViewModel ViewModel
        {
            get => (AddFilterViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}