using Newtonsoft.Json;

namespace DronZone_UWP.Models.Auth
{
    public class GetTokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public double ExpiresIn { get; set; }
    }
}