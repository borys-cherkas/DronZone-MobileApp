using System;
using System.Threading.Tasks;
using DronZone_UWP.Data.Api;
using DronZone_UWP.Data.Api.APIs;
using DronZone_UWP.Models.Auth;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Business.Services.Implementations
{
    internal class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private readonly IPreferencesService _preferencesService;
        private readonly IAuthRestApi _authApi;

        public AuthenticationService(IPreferencesService preferencesService, IAuthRestApi authApi)
        {
            _preferencesService = preferencesService;
            _authApi = authApi;
        }

        public async Task<bool> LoginAsync(string username, string password, bool rememberMe)
        {
            string errorMessage;
            try
            {
                var res = await RetrieveTokenAsync(username, password, rememberMe);
                if (res == null)
                {
                    return false;
                }

                await UpdateUserInfoAsync();
                _preferencesService.LastUpdateTokenTime = DateTime.Now;
                return true;
            }
            catch (ApiException ex)
            {
                if (ex.Message == "Bad Request")
                {
                    errorMessage = "Wrong username or password.";
                }
                else
                {
                    errorMessage = "Unauthorized";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            await ShowErrorAsync(errorMessage);
            return false;
        }

        private async Task<GetTokenModel> RetrieveTokenAsync(string username, string password, bool rememberMe)
        {
            var tokenInfoResponse = await _authApi.RetrieveTokenAsync(username, password, rememberMe);

            if (string.IsNullOrEmpty(tokenInfoResponse?.AccessToken))
            {
                await ShowErrorAsync("Something went wrong... Login failed.");
                return null;
            }

            _preferencesService.TokenInfo = tokenInfoResponse;
            return _preferencesService.TokenInfo;
        }

        public async Task<bool> RegisterAsync(RegistrationModel registrationModel)
        {
            string errorMessage;
            try
            {
                await RegisterInternalAsync(registrationModel);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            await ShowErrorAsync(errorMessage);
            return false;
        }

        private Task RegisterInternalAsync(RegistrationModel registrationModel)
        {
            return _authApi.RegisterAsync(registrationModel);
        }

        public async Task<UserInfoModel> UpdateUserInfoAsync()
        {
            string errorMessage = string.Empty;
            try
            {
                var userInfoResponse = await _authApi.GetCurrentUserAsync();
                if (!string.IsNullOrEmpty(userInfoResponse?.IdentityId))
                {
                    _preferencesService.UserInfo = userInfoResponse;
                    return userInfoResponse;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            await ShowErrorAsync(errorMessage);
            return null;
        }

        public void Logout()
        {
            _preferencesService.Clear();
        }
    }
}