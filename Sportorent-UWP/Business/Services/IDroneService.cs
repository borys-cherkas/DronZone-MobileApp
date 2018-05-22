using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Models.AthleticField;
using DronZone_UWP.Presentation.ViewModels.Drone;

namespace DronZone_UWP.Business.Services
{
    public interface IDroneService
    {
        Task<ICollection<UserDroneListViewModel>> GetUserDronesAsync();

        Task<UserDroneListViewModel> GetDetailedDroneAsync(string droneId);
    }
}
