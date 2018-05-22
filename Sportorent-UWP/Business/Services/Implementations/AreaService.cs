using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.APIs;
using DronZone_UWP.Models.Area;

namespace DronZone_UWP.Business.Services.Implementations
{
    public class AreaService : ServiceBase, IAreaService
    {
        private readonly IAreaRestApi _areaRestApi;

        public AreaService(IAreaRestApi areaRestApi)
        {
            _areaRestApi = areaRestApi;
        }

        public async Task<ICollection<AreaDetailedModel>> GetCurrentUserAreasAsync()
        {
            try
            {
                var response = await _areaRestApi.GetCurrentUserAreasAsync();
                return response;
            }
            catch(Exception ex)
            {
                await ShowErrorAsync(ex.Message);
                return null;
            }
        }

        public async Task<AreaDetailedModel> GetDetailedAreaAsync(string areaId)
        {
            try
            {
                var response = await _areaRestApi.GetDetailedAreaAsync(areaId);
                return response;
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
                return null;
            }
        }

        //public async Task<bool> AddReservationAsync(AddReservationModel model)
        //{
        //    var response = await _reservationsRestApi.AddReservationAsync(model);
        //    return await CheckResponseAsync(response);
        //}
    }
}