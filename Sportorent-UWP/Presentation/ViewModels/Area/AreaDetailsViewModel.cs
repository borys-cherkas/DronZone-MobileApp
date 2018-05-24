using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.Area;
using DronZone_UWP.Presentation.Views.Area;
using DronZone_UWP.Presentation.Views.AreaFilter;
using DronZone_UWP.Utils;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.Area
{
    public class AreaDetailsViewModel : ViewModelBase
    {
        private readonly IAreaService _areaService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private readonly string _areaId;

        private AreaDetailedModel _areaDetailedModel;

        public AreaDetailsViewModel(IAreaService areaService, MenuNavigationHelper menuNavigationHelper)
        {
            _areaService = areaService;
            _menuNavigationHelper = menuNavigationHelper;

            _areaId = MenuContentViewModel.Param as string;

            GoToFiltersCommand = ReactiveCommand.CreateFromTask(async () => GoToFiltersExecuted());

            GoBackToAreaListCommand = ReactiveCommand.CreateFromTask(async () => GoBackToAreaListExecuted());

            Init();
        }

        private void GoToFiltersExecuted()
        {
            _menuNavigationHelper.NavigateTo(typeof(AreaFilterListPage), _areaId);
        }

        private void GoBackToAreaListExecuted()
        {
            _menuNavigationHelper.NavigateTo(typeof(UserAreasListPage));
        }

        public AreaDetailedModel Area
        {
            get => _areaDetailedModel;
            set => this.RaiseAndSetIfChanged(ref _areaDetailedModel, value);
        }

        public Geopoint MapCenter
        {
            get
            {
                if (Area != null)
                {
                    var map = Area.MapRectangle;
                    var centerLatitude = (map.North + map.South) / 2;
                    var centerLongitude = (map.East + map.West) / 2;

                    return new Geopoint(new BasicGeoposition()
                    {
                        Latitude = centerLatitude,
                        Longitude = centerLongitude
                    });
                }

                return new Geopoint(new BasicGeoposition());
            }
        }

        public ReactiveCommand GoToFiltersCommand { get; }

        public ReactiveCommand GoBackToAreaListCommand { get; }

        private async void Init()
        {
            await LoadAreaDetailsAsync();
        }

        private async Task LoadAreaDetailsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                Area = await _areaService.GetDetailedAreaAsync(_areaId);
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }
            finally
            {
                OnIsInProgressChanges(false);
            }
        }
    }
}