using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.KindOfSport;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.KindOfSport
{
    public sealed partial class KindOfSportListPage : IViewFor<KindOfSportListViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", 
                typeof(KindOfSportListViewModel), 
                typeof(KindOfSportListPage), 
                new PropertyMetadata(default(KindOfSportListViewModel)));

        public KindOfSportListPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<KindOfSportListViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.KindOfSportList, v => v.KindsOfSportListView.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedKindOfSport, v => v.KindsOfSportListView.SelectedItem));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (KindOfSportListViewModel)value;
        }

        public KindOfSportListViewModel ViewModel
        {
            get => (KindOfSportListViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
