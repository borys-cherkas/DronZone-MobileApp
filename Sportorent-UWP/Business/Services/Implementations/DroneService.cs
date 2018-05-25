using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.APIs;
using DronZone_UWP.Models.Drone;

namespace DronZone_UWP.Business.Services.Implementations
{
    internal class DroneService : ServiceBase, IDroneService
    {
        private readonly IDroneRestApi _droneRestApi;

        public DroneService(IDroneRestApi droneRestApi)
        {
            _droneRestApi = droneRestApi;
        }

        public async Task<ICollection<DroneDetailedModel>> GetUserDronesAsync()
        {
            return await ExecuteSafeApiRequestAsync(
                async () => await _droneRestApi.GetUserDronesAsync());
        }

        public async Task<DroneDetailedModel> GetDetailedDroneAsync(string droneId)
        {
            return await ExecuteSafeApiRequestAsync(
                async () => await _droneRestApi.GetDetailedDroneAsync(droneId));
        }

        public async Task AttachDroneAsync(string code)
        {
            await ExecuteSafeApiRequestAsync(
                async () => await _droneRestApi.AttachDroneAsync(code));
        }
    }
}