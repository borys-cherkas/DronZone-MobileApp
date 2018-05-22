using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        private bool _isBusy;
        private readonly ContentDialog _contentDialog;

        protected ViewModelBase()
        {
            _contentDialog = new ContentDialog();
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        }

        protected async Task ShowErrorAsync(string message)
        {
            _contentDialog.Title = "Error";
            var textBlock = new TextBlock { Text = message };
            _contentDialog.Content = textBlock;

            _contentDialog.CloseButtonText = "OK";
            await _contentDialog.ShowAsync();
        }

        protected async Task ShowMessageAsync(string message)
        {
            _contentDialog.Title = "Info";
            var textBlock = new TextBlock { Text = message };
            _contentDialog.Content = textBlock;

            _contentDialog.CloseButtonText = "OK";
            await _contentDialog.ShowAsync();
        }

        protected void OnIsInProgressChanges(bool isBusy)
        {
            IsBusy = isBusy;
        }
    }
}
