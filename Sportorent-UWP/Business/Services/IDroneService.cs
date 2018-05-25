using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Models.Drone;

namespace DronZone_UWP.Business.Services
{
    public interface IDroneService
    {
        Task<ICollection<DroneDetailedModel>> GetUserDronesAsync();

        Task<DroneDetailedModel> GetDetailedDroneAsync(string droneId);
    }
}
