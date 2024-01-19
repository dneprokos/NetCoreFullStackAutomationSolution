using Newtonsoft.Json;

namespace Dneprokos.Movies.Api.Client.Models.Authorization
{
    public class AuthorizationApiModel
    {
        [JsonProperty("accessToken")]
        public string? AccessToken { get; set; }
    }
}
