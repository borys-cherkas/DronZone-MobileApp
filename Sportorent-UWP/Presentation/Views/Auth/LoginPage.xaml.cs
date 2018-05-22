using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Auth;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.Auth
{
    public sealed partial class LoginPage : IViewFor<LoginViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(LoginViewModel), typeof(LoginPage), new PropertyMetadata(default(LoginViewModel)));

        public LoginPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<LoginViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.Bind(ViewModel, vm => vm.LoginModel.Username, v => v.EmailTextBox.Text));
            d(this.Bind(ViewModel, vm => vm.LoginModel.Password, v => v.PasswordBox.Password));
            d(this.Bind(ViewModel, vm => vm.LoginModel.RememberMe, v => v.RememberMeCheckBox.IsChecked));

            d(this.BindCommand(ViewModel, vm => vm.SignInCommand, v => v.SignInButton));
            d(this.BindCommand(ViewModel, vm => vm.GoToRegisterFormCommand, v => v.RegisterButton));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (LoginViewModel)value;
        }

        public LoginViewModel ViewModel
        {
            get => (LoginViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
