using System;
using System.Linq;
using System.Threading.Tasks;
using DronZone_UWP.Models.Devices;
using DronZone_UWP.Utils;
using ReactiveUI;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Presentation.ViewModels.Devices
{
    public class DevicesListViewModel : ViewModelBase
    {
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private ReactiveList<DeviceListItemModel> _deviceList;
        private DeviceListItemModel _selectedDevice;

        public DevicesListViewModel(MenuNavigationHelper menuNavigationHelper)
        {
            _menuNavigationHelper = menuNavigationHelper;

            Init();
        }

        public ReactiveList<DeviceListItemModel> DeviceList
        {
            get => _deviceList;
            set => this.RaiseAndSetIfChanged(ref _deviceList, value);
        }

        public DeviceListItemModel SelectedDevice
        {
            get => _selectedDevice;
            set => this.RaiseAndSetIfChanged(ref _selectedDevice, value);
        }

        private async void Init()
        {
            this.ObservableForProperty(x => x.SelectedDevice)
                .Subscribe(args =>
                {
                    //TODO: on select device 
                });

            DeviceList = new ReactiveList<DeviceListItemModel>();

            await LoadDevicesAsync();
        }

        private async Task LoadDevicesAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                //var listWrapper = await _devicesService.GetDevicesAsync();
                //listWrapper.DeviceList = listWrapper.DeviceList.ToList();

                //DeviceList.Clear();
                //DeviceList.AddRange(listWrapper.DeviceList);
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
