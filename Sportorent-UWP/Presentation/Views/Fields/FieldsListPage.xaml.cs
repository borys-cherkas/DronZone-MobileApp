using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.AthleticField;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.Fields
{
    public sealed partial class FieldsListPage : IViewFor<AthleticFieldListViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                typeof(AthleticFieldListViewModel),
                typeof(FieldsListPage),
                new PropertyMetadata(default(AthleticFieldListViewModel)));

        public FieldsListPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<AthleticFieldListViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.AthleticFieldList, v => v.KindsOfSportListView.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedAthleticField, v => v.KindsOfSportListView.SelectedItem));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AthleticFieldListViewModel)value;
        }

        public AthleticFieldListViewModel ViewModel
        {
            get => (AthleticFieldListViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
