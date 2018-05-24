using System;
using System.Linq;
using Windows.Storage;
using DronZone_UWP.Models.Auth;
using Newtonsoft.Json;

namespace DronZone_UWP.Business.Services.Implementations
{
    class PreferencesService : IPreferencesService
    {
        private readonly ApplicationDataContainer _localSettings;

        private const string TokenInfoKey = "tokenInfo";
        private const string UserInfoKey = "userInfo";
        private const string LastUpdateTokenTimeKey = "lastUpdateTokenTime";

        public PreferencesService()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }

        public DateTime LastUpdateTokenTime
        {
            get => TryGetValue<DateTime>(LastUpdateTokenTimeKey);
            set => AddOrUpdateValue(LastUpdateTokenTimeKey, value);
        }

        public GetTokenModel TokenInfo
        {
            get => TryGetValue<GetTokenModel>(TokenInfoKey);
            set => AddOrUpdateValue(TokenInfoKey, value);
        }

        public UserInfoModel UserInfo
        {
            get => TryGetValue<UserInfoModel>(UserInfoKey);
            set => AddOrUpdateValue(UserInfoKey, value);
        }

        public bool IsLoggedIn
        {
            get
            {
                if (string.IsNullOrEmpty(AccessToken))
                    return false;

                DateTime tokenExpiresAt = DateTime.Now.AddSeconds(TokenInfo.ExpiresIn);
                if (tokenExpiresAt <= DateTime.Now)
                    return false;

                return true;
            }
        }

        public string AccessToken
        {
            get => TokenInfo?.AccessToken;
            set
            {
                var tokenInfo = TokenInfo;

                tokenInfo.AccessToken = value;

                TokenInfo = tokenInfo;
            }
        }

        public void Clear()
        {
            _localSettings.Values.Clear();
        }

        private void AddOrUpdateValue<T>(string key, T value)
        {
            if (_localSettings.Values.Keys.Any(x => x == key))
            {
                _localSettings.Values[key] = Serialize(value);
            }
            else
            {
                _localSettings.Values.Add(key, Serialize(value));
            }
        }

        private T TryGetValue<T>(string key)
        {
            if (_localSettings.Values.Keys.Any(x => x == key))
            {
                var value = _localSettings.Values[key];
                return value != null ? Deserialize<T>(_localSettings.Values[key].ToString()) : default(T);
            }

            return default(T);
        }

        private string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}