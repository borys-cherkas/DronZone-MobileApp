using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.APIs;
using DronZone_UWP.Models.AthleticField;
using DronZone_UWP.Presentation.ViewModels.Drone;

namespace DronZone_UWP.Business.Services.Implementations
{
    internal class DroneService : ServiceBase, IDroneService
    {
        private readonly IDroneRestApi _droneRestApi;

        public DroneService(IDroneRestApi droneRestApi)
        {
            _droneRestApi = droneRestApi;
        }

        public async Task<ICollection<UserDroneListViewModel>> GetUserDronesAsync()
        {
            var response = await _droneRestApi.GetUserDronesAsync();
            return response;
        }

        public async Task<UserDroneListViewModel> GetDetailedDroneAsync(string droneId)
        {
            var response = await _droneRestApi.GetDetailedDroneAsync(droneId);
            return response;
        }
    }
}