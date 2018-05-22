using System.Threading.Tasks;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Data.Api.APIs
{
    public interface IBillingRestApi
    {
        Task<ResponseWrapper> PayBill(int billId);
    }
}
