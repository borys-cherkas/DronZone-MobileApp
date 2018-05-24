using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Autofac;
using DronZone_UWP.Data.Api;
using DronZone_UWP.Utils;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Business.Services.Implementations
{
    public abstract class ServiceBase
    {
        private readonly ContentDialog _contentDialog;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        protected ServiceBase()
        {
            _contentDialog = new ContentDialog();
            _menuNavigationHelper = App.Container.Resolve<MenuNavigationHelper>();
        }

        protected async Task<T> ExecuteSafeApiRequestAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (ApiUnauthorizedException)
            {
                await ShowErrorAsync("You have been unauthorized. Please, login again.");
                _menuNavigationHelper.NavigateToLoginPage();
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }

            return default(T);
        }

        protected async Task ShowErrorAsync(string message)
        {
            _contentDialog.Title = "Error";
            var textBlock = new TextBlock { Text = message };
            _contentDialog.Content = textBlock;

            _contentDialog.CloseButtonText = "OK";
            await _contentDialog.ShowAsync();
        }
    }
}
