using Newtonsoft.Json;

namespace Dneprokos.Movies.Api.Client.Models.Users
{
    public class UsersApiModel
    {
        public List<UserApiModel>? Users { get; set; }
    }

    public class UserApiModel
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("role")]
        public string? Role { get; set; }
    }
}
