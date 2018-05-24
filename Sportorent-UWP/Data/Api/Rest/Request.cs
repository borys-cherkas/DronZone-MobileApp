using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Sportorent_UWP;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Data.Api.Rest
{
    internal class Request
    {
        private readonly RestApiBase _restApiBase;
        private readonly string _url;
        private readonly IList<KeyValuePair<string, string>> _queryItems;
        private readonly IPreferencesService _preferencesService;

        public Request(RestApiBase restApiBase, string url, IList<KeyValuePair<string, string>> queryItems)
        {
            _restApiBase = restApiBase;
            _url = url;
            _queryItems = queryItems;
            _preferencesService = App.Container.Resolve<IPreferencesService>();
        }

        internal async Task<T> Get<T>()
        {
            using (var httpClient = CreateHttpClient())
            {
                ProcessInterceptors(httpClient);

                var response = CheckResponse(await httpClient.GetAsync(PrepareUrl()));

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        internal async Task<T> PostFormUrlEncoded<T>(IList<KeyValuePair<string, string>> @params)
        {
            using (var httpClient = CreateHttpClient())
            {
                ProcessInterceptors(httpClient);

                var response = CheckResponse(await httpClient.PostAsync(_url, new FormUrlEncodedContent(@params)));

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        private HttpResponseMessage CheckResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var if401 = response?.RequestMessage?.RequestUri?.ToString()?.Contains("Account/Login?ReturnUrl");
                if (if401.HasValue && if401.Value)
                {
                    throw new ApiUnauthorizedException();
                }

                throw new ApiException(response.ReasonPhrase);
            }

            return response;
        }

        private void ProcessInterceptors(HttpClient httpClient)
        {
            if (_preferencesService.IsLoggedIn)
            {
                var tokenInfo = _preferencesService.TokenInfo;
                var authHeader = new KeyValuePair<string, string>("Authorization",
                    $"{tokenInfo.TokenType} {tokenInfo.AccessToken}");
                _restApiBase.AddInterceptor(new RequestInterceptorBase(authHeader));
            }

            _restApiBase.CallInterceptors(httpClient);
        }

        private HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = _restApiBase.BaseAddress
            };
        }

        private string PrepareUrl()
        {
            StringBuilder sb = new StringBuilder(_url);

            if (_queryItems.Count > 0)
            {
                sb.Append('?').Append(string.Join("&", _queryItems.Select(it => $"{it.Key}={it.Value}")));
            }

            return sb.ToString();
        }
    }
}