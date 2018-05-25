using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Models.Drone;
using DronZone_UWP.Presentation.ViewModels.Drone;

namespace DronZone_UWP.Data.Api.APIs
{
    public interface IDroneRestApi
    {
        Task<ICollection<DroneDetailedModel>> GetUserDronesAsync();

        Task<DroneDetailedModel> GetDetailedDroneAsync(string droneId);

        Task AttachDroneAsync(string code);
    }
}
