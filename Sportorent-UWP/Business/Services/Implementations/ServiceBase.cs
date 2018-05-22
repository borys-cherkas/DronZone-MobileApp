using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Business.Services.Implementations
{
    public abstract class ServiceBase
    {
        private readonly ContentDialog _contentDialog;

        protected ServiceBase()
        {
            _contentDialog = new ContentDialog();
        }

        protected async Task<bool> CheckResponseAsync(ResponseWrapper response)
        {
            if (!response.IsValid)
            {
                await ShowErrorAsync(response.ErrorMessage);
                return false;
            }

            return true;
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
