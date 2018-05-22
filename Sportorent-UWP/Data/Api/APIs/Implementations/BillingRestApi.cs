using System;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.Rest;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Data.Api.APIs.Implementations
{
    public class BillingRestApi : RestApiBase, IBillingRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "Billing";

        public BillingRestApi() : base(new Uri(BaseApiAddress)) { }
        
        public Task<ResponseWrapper> PayBill(int billId)
        {
            return Url($"{ControllerPath}/PayBill")
                .FormUrlEncoded()
                .Param("BillId", billId.ToString())
                .PostAsync<ResponseWrapper>();
        }
    }
}
