using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.Rest;
using DronZone_UWP.Models.Drone;

namespace DronZone_UWP.Data.Api.APIs.Implementations
{
    public class DroneRestApi : RestApiBase, IDroneRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "UserDrones";

        public DroneRestApi() : base(new Uri(BaseApiAddress)) { }

        public Task<ICollection<DroneDetailedModel>> GetUserDronesAsync()
        {
            return Url($"{ControllerPath}/GetUserDrones")
                .GetAsync<ICollection<DroneDetailedModel>>();
        }

        public Task<DroneDetailedModel> GetDetailedDroneAsync(string droneId)
        {
            return Url($"{ControllerPath}/GetById/{droneId}")
                .GetAsync<DroneDetailedModel>();
        }
    }
}
