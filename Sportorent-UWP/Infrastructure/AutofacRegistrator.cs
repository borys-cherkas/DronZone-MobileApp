using Autofac;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Business.Services.Implementations;
using DronZone_UWP.Data.Api.APIs;
using DronZone_UWP.Data.Api.APIs.Implementations;
using DronZone_UWP.Presentation.ViewModels;
using DronZone_UWP.Presentation.ViewModels.Area;
using DronZone_UWP.Presentation.ViewModels.AreaFilters;
using DronZone_UWP.Presentation.ViewModels.Auth;
using DronZone_UWP.Presentation.ViewModels.Drone;
using DronZone_UWP.Utils;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Infrastructure
{
    public static class AutofacRegistrator
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterApis(builder);
            RegisterViewModels(builder);
            RegisterUtils(builder);
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<LoginViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<MenuContentViewModel>().AsSelf().AsImplementedInterfaces();
            
            builder.RegisterType<UserDroneListViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<DroneDetailsViewModel>().AsSelf().AsImplementedInterfaces();

            builder.RegisterType<AddFilterViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<FilterListViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<FilterDetailsViewModel>().AsSelf().AsImplementedInterfaces();

            builder.RegisterType<UserAreasListViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<AreaDetailsViewModel>().AsSelf().AsImplementedInterfaces();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PreferencesService>().As<IPreferencesService>();
            builder.RegisterType<NetworkService>().As<INetworkService>();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<DroneService>().As<IDroneService>();
            builder.RegisterType<AreaService>().As<IAreaService>();
            builder.RegisterType<AreaFilterService>().As<IAreaFilterService>();
        }

        private static void RegisterApis(ContainerBuilder builder)
        {
            builder.RegisterType<AuthRestApi>().As<IAuthRestApi>();
            builder.RegisterType<DroneRestApi>().As<IDroneRestApi>();
            builder.RegisterType<AreaRestApi>().As<IAreaRestApi>();
            builder.RegisterType<FilterRestApi>().As<IFilterRestApi>();
        }

        private static void RegisterUtils(ContainerBuilder builder)
        {
            builder.RegisterType<MenuNavigationHelper>().AsSelf().SingleInstance();
        }
    }
}
