using System;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api.Rest;
using DronZone_UWP.Models.Auth;

namespace DronZone_UWP.Data.Api.APIs.Implementations
{
    public class AuthRestApi : RestApiBase, IAuthRestApi
    {
        private const string BaseApiAddress = ApiRouting.ApiServiceAddress;

        public AuthRestApi() : base(new Uri(BaseApiAddress)) { }

        public Task RegisterAsync(RegistrationModel registrationModel)
        {
            return Url("api/account/register")
                .FormUrlEncoded()
                .Param(nameof(registrationModel.Email), registrationModel.Email)
                .Param(nameof(registrationModel.Password), registrationModel.Password)
                .Param(nameof(registrationModel.FirstName), registrationModel.FirstName)
                .Param(nameof(registrationModel.LastName), registrationModel.LastName)
                .PostAsync<GetTokenModel>();
        }

        public Task<GetTokenModel> RetrieveTokenAsync(string username, string password, bool rememberMe)
        {
            return Url("connect/token")
                .FormUrlEncoded()
                .Param("grant_type", "password")
                .Param(nameof(username), username)
                .Param(nameof(password), password)
                .PostAsync<GetTokenModel>();
        }

        public Task<UserInfoModel> GetCurrentUserAsync()
        {
            return Url("api/account/GetUserInfo")
                .GetAsync<UserInfoModel>();
        }
    }
}
