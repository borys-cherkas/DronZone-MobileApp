using System;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.Rest;
using DronZone_UWP.Models.Devices;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Data.Api.APIs.Implementations
{
    public class DevicesRestApi : RestApiBase, IDevicesRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "IoTDevices";

        public DevicesRestApi() : base(new Uri(BaseApiAddress)) { }

        public Task<ResponseWrapper<DeviceListModel>> GetDevicesAsync()
        {
            return Url($"{ControllerPath}/GetDevices")
                .GetAsync<ResponseWrapper<DeviceListModel>>();
        }

        public Task<ResponseWrapper<DeviceListModel>> GetUnlinkedDevices()
        {
            return Url($"{ControllerPath}/GetUnlinkedDevices")
                .GetAsync<ResponseWrapper<DeviceListModel>>();
        }

        public Task<ResponseWrapper> LinkDeviceToFieldAsync(string deviceId, int fieldId)
        {
            return Url($"{ControllerPath}/LinkToTheField")
                .FormUrlEncoded()
                .Param("IotDeviceId", deviceId)
                .Param("AthleticFieldId", fieldId.ToString())
                .PostAsync<ResponseWrapper>();
        }
    }
}
