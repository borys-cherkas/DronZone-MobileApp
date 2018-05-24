using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Models.AreaFilters;

namespace DronZone_UWP.Data.Api.APIs
{
    public interface IFilterRestApi
    {
        Task<ICollection<AreaFilterDetailedModel>> GetFiltersByAreaIdAsync(string areaId);

        Task<AreaFilterDetailedModel> GetFilterByIdAsync(int filterId);

        Task AddFilterAsync(AddFilterDetailedModel filterModel);
    }
}
