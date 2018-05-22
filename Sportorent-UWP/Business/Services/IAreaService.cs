using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Models.Area;

namespace DronZone_UWP.Business.Services
{
    public interface IAreaService
    {
        Task<ICollection<AreaDetailedModel>> GetCurrentUserAreasAsync();
        
        Task<AreaDetailedModel> GetDetailedAreaAsync(string areaId);

        //Task<bool> AddReservationAsync(AddReservationModel model);
    }
}
