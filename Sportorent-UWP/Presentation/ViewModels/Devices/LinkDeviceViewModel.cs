using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.AthleticField;
using DronZone_UWP.Models.Devices;
using DronZone_UWP.Models.KindOfSport;
using DronZone_UWP.Presentation.Views.Devices;
using DronZone_UWP.Utils;
using ReactiveUI;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Presentation.ViewModels.Devices
{
    public class LinkDeviceViewModel : ViewModelBase
    {
        private readonly IDroneService _athleticFieldsService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        public LinkDeviceViewModel(MenuNavigationHelper menuNavigationHelper,
            IDroneService athleticFieldsService)
        {
            _athleticFieldsService = athleticFieldsService;
            _menuNavigationHelper = menuNavigationHelper;
            
            SaveCommand = ReactiveCommand.CreateFromTask(LinkFieldToDeviceAsync);

            Init();
        }

        private async void Init()
        {
            await Task.WhenAll(new List<Task>
            {
                LoadDevicesAsync(),
                UpdateSportsAsync()
            });

            this.ObservableForProperty(x => x.SelectedSport)
                .Subscribe(async x =>
            {
                if (x.Value != null)
                {
                    await UpdateFieldsAsync();
                }
                else
                {
                    SelectedField = null;
                    Fields.Clear();
                }
            });
        }

        private async Task UpdateSportsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                //var kindOfSports = await _kindsOfSportService.GetAvailableKindsOfSportAsync();
                //Sports.Clear();
                //Sports.AddRange(kindOfSports.KindOfSportList);
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

        private async Task UpdateFieldsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                //var fields = await _athleticFieldsService.GetUnlinkedAthleticFieldsBySportKind(SelectedSport.Id);
                //SelectedField = null;
                //Fields.Clear();
                //Fields.AddRange(fields.AthleticFieldList);
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

        private async Task LoadDevicesAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                //var devices = await _devicesService.GetUnlinkedDevices();
                //Devices.Clear();
                //Devices.AddRange(devices.DeviceList);
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

        public ReactiveList<DeviceListItemModel> Devices { get; set; } =
            new ReactiveList<DeviceListItemModel>();

        public ReactiveList<AthleticFieldListItemModel> Fields { get; set; } =
            new ReactiveList<AthleticFieldListItemModel>();

        public ReactiveList<KindOfSportListItemModel> Sports { get; set; } =
            new ReactiveList<KindOfSportListItemModel>();

        private DeviceListItemModel _selectedDeviceId;
        public DeviceListItemModel SelectedDeviceId
        {
            get { return _selectedDeviceId; }
            set { this.RaiseAndSetIfChanged(ref _selectedDeviceId, value); }
        }

        private AthleticFieldListItemModel _selectedField;
        public AthleticFieldListItemModel SelectedField
        {
            get { return _selectedField; }
            set { this.RaiseAndSetIfChanged(ref _selectedField, value); }
        }

        private KindOfSportListItemModel _selectedSport;
        public KindOfSportListItemModel SelectedSport
        {
            get { return _selectedSport; }
            set { this.RaiseAndSetIfChanged(ref _selectedSport, value); }
        }

        private string _fieldError = "";
        public string FieldError
        {
            get { return _fieldError; }
            set { this.RaiseAndSetIfChanged(ref _fieldError, value); }
        }

        private string _deviceIdError = "";
        public string DeviceIdError
        {
            get { return _deviceIdError; }
            set { this.RaiseAndSetIfChanged(ref _deviceIdError, value); }
        }

        private ReactiveCommand _saveCommand;
        public ReactiveCommand SaveCommand
        {
            get { return _saveCommand; }
            set { this.RaiseAndSetIfChanged(ref _saveCommand, value); }
        }

        private bool Validate()
        {
            FieldError = DeviceIdError = "";

            if (SelectedField == null)
            {
                FieldError = "Field is required.";
                return false;
            }

            if (SelectedDeviceId == null)
            {
                DeviceIdError = "Device Id is required.";
                return false;
            }

            return true;
        }

        private async Task LinkFieldToDeviceAsync()
        {
            if (!Validate())
            {
                return;
            }

            OnIsInProgressChanges(true);

            try
            {
                //await _devicesService.LinkDeviceToFieldAsync(SelectedDeviceId.Id.ToString(), SelectedField.Id);
                _menuNavigationHelper.NavigateTo(typeof(DevicesListPage));

                await ShowMessageAsync("Device was linked to the athletic field successfully.");
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
