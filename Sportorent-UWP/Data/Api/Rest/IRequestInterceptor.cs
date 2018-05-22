using System.Net.Http;

namespace DronZone_UWP.Data.Api.Rest
{
    public interface IRequestInterceptor
    {
        void RemoveInterceptorIfExist(string key);

        void Intercept(HttpClient httpClient);
    }
}
