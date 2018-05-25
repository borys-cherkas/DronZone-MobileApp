using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.AreaFilters;
using ReactiveUI;

namespace DronZone_UWP.Presentation.Views.AreaFilter
{
    public sealed partial class AreaFilterListPage : IViewFor<FilterListViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(FilterListViewModel),
                typeof(AreaFilterListPage),
                new PropertyMetadata(default(FilterListViewModel)));

        public AreaFilterListPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<FilterListViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.AreaFilterList, v => v.FilterListView.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedFilter, v => v.FilterListView.SelectedItem));

            d(this.BindCommand(ViewModel, vm => vm.GoToAddFilterPageCommand, v => v.AddNewFitlerButton));
            d(this.BindCommand(ViewModel, vm => vm.GoBackToAreaDetailsCommand, v => v.GoToAreaDetailsButton));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (FilterListViewModel)value;
        }

        public FilterListViewModel ViewModel
        {
            get => (FilterListViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}