using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Models.Area;

namespace DronZone_UWP.Data.Api.APIs
{
    public interface IAreaRestApi
    {
        Task<ICollection<AreaDetailedModel>> GetCurrentUserAreasAsync();

        Task<AreaDetailedModel> GetDetailedAreaAsync(string areaId);
    }
}
