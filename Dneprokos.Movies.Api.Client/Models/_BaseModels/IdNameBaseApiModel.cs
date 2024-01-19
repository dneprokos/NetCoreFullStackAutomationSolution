using Newtonsoft.Json;

namespace Dneprokos.Movies.Api.Client.Models._BaseModels
{
    public class IdNameBaseApiModel
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
