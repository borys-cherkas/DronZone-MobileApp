using System.Threading.Tasks;
using DronZone_UWP.Models.Auth;

namespace Sportorent_UWP.Business.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LoginAsync(string username, string password, bool rememberMe);

        Task<bool> RegisterAsync(RegistrationModel registrationModel);

        Task<UserInfoModel> UpdateUserInfoAsync();
        
        void Logout();
    }
}
