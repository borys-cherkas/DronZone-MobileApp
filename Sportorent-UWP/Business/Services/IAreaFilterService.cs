using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DronZone_UWP.Models.AreaFilters;

namespace DronZone_UWP.Business.Services
{
    public interface IAreaFilterService
    {
        Task<ICollection<AreaFilterDetailedModel>> GetFiltersByAreaAsync(string areaId);

        Task CreateFilterAsync(AddFilterDetailedModel filterModel);

        Task<AreaFilterDetailedModel> GetFilterAsync(int filterId);
    }
}
