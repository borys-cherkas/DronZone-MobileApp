using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.AppMenuContainer
{
    public sealed partial class AppMenuContainerPage : IViewFor<MenuContentViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MenuContentViewModel), typeof(AppMenuContainerPage), new PropertyMetadata(default(MenuContentViewModel)));

        public AppMenuContainerPage()
        {
            this.InitializeComponent();
            ViewModel = App.Container.Resolve<MenuContentViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.IsPaneOpened, v => v.HamburgerSplitView.IsPaneOpen));
            d(this.BindCommand(ViewModel, vm => vm.OpenClosePaneCommand, v => v.OpenClosePaneButton));

            d(this.OneWayBind(ViewModel, vm => vm.MenuItems, v => v.MenuItemsItemsControl.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedMenuItem, v => v.MenuItemsItemsControl.SelectedItem));

            d(this.Bind(ViewModel, vm => vm.CurrentPage, v => v.AppMenuInternalFrame.SourcePageType));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MenuContentViewModel)value;
        }

        public MenuContentViewModel ViewModel
        {
            get => (MenuContentViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
