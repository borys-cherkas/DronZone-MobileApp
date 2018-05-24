using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.APIs;
using DronZone_UWP.Models.AreaFilters;

namespace DronZone_UWP.Business.Services.Implementations
{
    public class AreaFilterService : ServiceBase, IAreaFilterService
    {
        private readonly IFilterRestApi _filtersRestApi;

        public AreaFilterService(IFilterRestApi filtersRestApi)
        {
            _filtersRestApi = filtersRestApi;
        }

        public async Task<ICollection<AreaFilterDetailedModel>> GetFiltersByAreaAsync(string areaId)
        {
            return await ExecuteSafeApiRequestAsync(
                async () => await _filtersRestApi.GetFiltersByAreaIdAsync(areaId));
        }

        public async Task<AreaFilterDetailedModel> GetFilterAsync(int filterId)
        {
            return await ExecuteSafeApiRequestAsync(
                async () => await _filtersRestApi.GetFilterByIdAsync(filterId));
        }

        public async Task CreateFilterAsync(AddFilterDetailedModel filterModel)
        {
            await ExecuteSafeApiRequestAsync(
                async () => await _filtersRestApi.AddFilterAsync(filterModel));
        }
    }
}
