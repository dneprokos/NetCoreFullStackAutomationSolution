using Newtonsoft.Json;

namespace Dneprokos.Movies.Api.Client.Models._BaseModels
{
    public class PaginationApiModel<T>
    {
        [JsonProperty("data")]
        public List<T>? Data { get; set; }

        [JsonProperty("page_number")]
        public int? PageNumber { get; set; }

        [JsonProperty("page_limit")]
        public int? PageLimit { get; set; }

        [JsonProperty("total_results")]
        public int? TotalResults { get; set; }
    }
}
