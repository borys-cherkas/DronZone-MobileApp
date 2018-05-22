using System.Threading.Tasks;
using DronZone_UWP.Models.Devices;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Data.Api.APIs
{
    public interface IDevicesRestApi
    {
        Task<ResponseWrapper<DeviceListModel>> GetDevicesAsync();

        Task<ResponseWrapper<DeviceListModel>> GetUnlinkedDevices();

        Task<ResponseWrapper> LinkDeviceToFieldAsync(string deviceId, int fieldId);
    }
}
