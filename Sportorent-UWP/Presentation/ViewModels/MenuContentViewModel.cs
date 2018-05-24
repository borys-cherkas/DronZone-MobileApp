using System;
using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Constants;
using DronZone_UWP.Presentation.Views.Area;
using DronZone_UWP.Presentation.Views.Auth;
using DronZone_UWP.Presentation.Views.Drones;
using DronZone_UWP.Utils;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels
{
    public class MenuContentViewModel : ViewModelBase
    {
        private readonly IPreferencesService _preferencesService;
        private readonly MenuNavigationHelper _menuNavigationHelper;
        
        private bool _isPaneOpened;
        private MenuItemViewModel _selectedMenuItem;
        private Type _currentPage;

        public MenuContentViewModel(
            IPreferencesService preferencesService,
            MenuNavigationHelper menuNavigationHelper)
        {
            _preferencesService = preferencesService;
            _menuNavigationHelper = menuNavigationHelper;

            OpenClosePaneCommand = ReactiveCommand.Create(OpenClosePaneCommandExecuted);

            this.ObservableForProperty(x => x.SelectedMenuItem)
                .Subscribe(args => OnSelectedMenuItemChanged(args.Value));

            menuNavigationHelper.ObservableForProperty(x => x.CurrentPageType)
                .Where(args => args.Value != null)
                .Subscribe(args => OnSelectedMenuItemChangedInternal(args.Value, menuNavigationHelper.Param));

            FillMenuItems();
            SelectedMenuItem = MenuItems.First();
        }

        public static object Param { get; set; }

        public ReactiveList<MenuItemViewModel> MenuItems { get; set; } = new ReactiveList<MenuItemViewModel>();

        public Type CurrentPage
        {
            get => _currentPage;
            set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }

        public MenuItemViewModel SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => this.RaiseAndSetIfChanged(ref _selectedMenuItem, value);
        }

        public bool IsPaneOpened
        {
            get => _isPaneOpened;
            set => this.RaiseAndSetIfChanged(ref _isPaneOpened, value);
        }

        public ReactiveCommand OpenClosePaneCommand { get; }


        public void OnSelectedMenuItemChanged(MenuItemViewModel item)
        {
            _menuNavigationHelper.NavigateTo(item.PageType);
            IsPaneOpened = false;
        }

        private void OnSelectedMenuItemChangedInternal(Type pageType, object param = null)
        {
            if (pageType != typeof(LoginPage)
                && pageType != typeof(RegistrationPage))
            {
                Param = param;
                CurrentPage = pageType;
            }
            else
            {
                var frame = (Window.Current.Content as Frame);
                frame?.Navigate(pageType, param);
                _preferencesService.Clear();
            }
        }

        private void FillMenuItems()
        {
            MenuItems.Clear();

            //MenuItems.Add(new MenuItemViewModel
            //{
            //    DisplayName = "All Kinds Of Sport",
            //    Icon = "\xE8FD",
            //    PageType = typeof(KindOfSportListPage)
            //});

            string userRole = _preferencesService.UserInfo?.Roles;
            if (userRole == RolesConstants.User)
            {
                MenuItems.Add(new MenuItemViewModel
                {
                    DisplayName = "My Areas",
                    Icon = "\xE728",
                    PageType = typeof(UserAreasListPage)
                });
                MenuItems.Add(new MenuItemViewModel
                {
                    DisplayName = "My Drones",
                    Icon = "\xE728",
                    PageType = typeof(UserDronesListPage)
                });
            }

            if (userRole == RolesConstants.Administrator)
            {
                //TODO: Remove
                MenuItems.Add(new MenuItemViewModel
                {
                    DisplayName = "My Drones",
                    Icon = "\xE728",
                    PageType = typeof(UserDronesListPage)
                });
            }

            // TODO: link to the website
            //MenuItems.Add(new MenuItemViewModel
            //{
            //    DisplayName = "Visit WebSite",
            //    Icon = "\xE946",
            //    PageType = typeof(AboutPage)
            //});

            MenuItems.Add(new MenuItemViewModel
            {
                DisplayName = _preferencesService.IsLoggedIn ? "Logout" : "Login",
                Icon = "\xE726",
                PageType = typeof(LoginPage)
            });
        }

        private void OpenClosePaneCommandExecuted()
        {
            IsPaneOpened = !IsPaneOpened;
        }
    }

    public class MenuItemViewModel : ReactiveObject
    {
        public string DisplayName { get; set; }

        public string Icon { get; set; }

        public Type PageType { get; set; }
    }
}
