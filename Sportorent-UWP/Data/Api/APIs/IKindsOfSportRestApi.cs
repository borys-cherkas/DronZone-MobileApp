using System.Threading.Tasks;
using DronZone_UWP.Models.KindOfSport;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Data.Api.APIs
{
    public interface IKindsOfSportRestApi
    {
        Task<ResponseWrapper<KindOfSportListModel>> GetAvailableKindsOfSportsAsync();
    }
}
