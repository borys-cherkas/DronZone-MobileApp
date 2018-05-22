using System;
using Windows.UI.Xaml;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Auth;
using ReactiveUI;
using Sportorent_UWP;

namespace DronZone_UWP.Presentation.Views.Auth
{
    public sealed partial class RegistrationPage : IViewFor<RegistrationViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", 
                typeof(RegistrationViewModel), 
                typeof(RegistrationPage), 
                new PropertyMetadata(default(RegistrationViewModel)));

        public RegistrationPage()
        {
            this.InitializeComponent();
            ViewModel = App.Container.Resolve<RegistrationViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.Bind(ViewModel, vm => vm.RegistrationModel.Email, v => v.EmailTextBox.Text));
            d(this.Bind(ViewModel, vm => vm.RegistrationModel.Password, v => v.PasswordBox.Password));
            d(this.Bind(ViewModel, vm => vm.RegistrationModel.FirstName, v => v.FirstNameTextBox.Text));
            d(this.Bind(ViewModel, vm => vm.RegistrationModel.LastName, v => v.LastNameTextBox.Text));
            
            d(this.BindCommand(ViewModel, vm => vm.RegisterCommand, v => v.RegisterButton));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (RegistrationViewModel)value;
        }

        public RegistrationViewModel ViewModel
        {
            get => (RegistrationViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
