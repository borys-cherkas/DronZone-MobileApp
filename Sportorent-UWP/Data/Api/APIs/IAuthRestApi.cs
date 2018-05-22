using System.Threading.Tasks;
using DronZone_UWP.Models.Auth;
using Sportorent_UWP.Models;

namespace DronZone_UWP.Data.Api.APIs
{
    public interface IAuthRestApi
    {
        Task RegisterAsync(RegistrationModel registrationModel);

        Task<GetTokenModel> RetrieveTokenAsync(string username, string password, bool rememberme);

        Task<UserInfoModel> GetCurrentUserAsync();
    }
}
