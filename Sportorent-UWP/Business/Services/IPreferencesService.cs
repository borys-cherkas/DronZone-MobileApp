using System;
using DronZone_UWP.Models.Auth;

namespace DronZone_UWP.Business.Services
{
    public interface IPreferencesService
    {
        DateTime LastUpdateTokenTime { get; set; }

        GetTokenModel TokenInfo { get; set; }

        UserInfoModel UserInfo { get; set; }

        string AccessToken { get; }

        bool IsLoggedIn { get; }

        void Clear();
    }
}