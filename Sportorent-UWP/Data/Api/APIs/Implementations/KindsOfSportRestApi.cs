using System;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.Rest;
using DronZone_UWP.Models.KindOfSport;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Data.Api.APIs.Implementations
{
    public class KindsOfSportRestApi : RestApiBase, IKindsOfSportRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "KindsOfSports";

        public KindsOfSportRestApi() : base(new Uri(BaseApiAddress)) { }
        
        public Task<ResponseWrapper<KindOfSportListModel>> GetAvailableKindsOfSportsAsync()
        {
            return Url($"{ControllerPath}/GetAvailableKindsOfSports")
                .GetAsync<ResponseWrapper<KindOfSportListModel>>();
        }
    }
}
