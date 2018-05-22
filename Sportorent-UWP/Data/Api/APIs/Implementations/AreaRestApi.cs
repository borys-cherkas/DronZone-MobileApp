using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.Rest;
using DronZone_UWP.Models.Area;

namespace DronZone_UWP.Data.Api.APIs.Implementations
{
    public class AreaRestApi : RestApiBase, IAreaRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "Zones";

        public AreaRestApi() : base(new Uri(BaseApiAddress)) { }

        public async Task<ICollection<AreaDetailedModel>> GetCurrentUserAreasAsync()
        {
            return await Url($"{ControllerPath}/GetAllUserZones")
                .GetAsync<ICollection<AreaDetailedModel>>();
        }

        public Task<AreaDetailedModel> GetDetailedAreaAsync(string areaId)
        {
            return Url($"{ControllerPath}/GetById/{areaId}")
                .GetAsync<AreaDetailedModel>();
        }

        //public Task<ResponseWrapper> AddReservationAsync(AddReservationModel model)
        //{
        //    return Url($"{ControllerPath}/AddReservation")
        //        .FormUrlEncoded()
        //        .Param(nameof(model.AthleticFieldId), model.AthleticFieldId.ToString())
        //        .Param(nameof(model.StartsAt), model.StartsAt.ToString("s"))
        //        .Param(nameof(model.EndsAt), model.EndsAt.ToString("s"))
        //        .PostAsync<ResponseWrapper>();
        //}
    }
}
