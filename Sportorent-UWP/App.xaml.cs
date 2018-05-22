using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Autofac;
using DronZone_UWP.Infrastructure;
using DronZone_UWP.Presentation.Views.AppMenuContainer;
using DronZone_UWP.Presentation.Views.Auth;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP
{
    public sealed partial class App : Application
    {
        static App()
        {
            var builder = new ContainerBuilder();
            AutofacRegistrator.RegisterTypes(builder);
            Container = builder.Build();
        }

        public static IContainer Container { get; }

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                // Создание фрейма, который станет контекстом навигации, и переход к первой странице
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Загрузить состояние из ранее приостановленного приложения
                }

                // Размещение фрейма в текущем окне
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    var prefService = Container.Resolve<IPreferencesService>();
                    bool isLogged = prefService.IsLoggedIn;
                    Type initialPage = isLogged ? typeof(AppMenuContainerPage) : typeof(LoginPage);
                    rootFrame.Navigate(initialPage, e.Arguments);
                }

                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Сохранить состояние приложения и остановить все фоновые операции
            deferral.Complete();
        }
    }
}
