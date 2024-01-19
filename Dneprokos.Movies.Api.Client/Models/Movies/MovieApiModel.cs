using Dneprokos.Movies.Api.Client.Models._BaseModels;
using Newtonsoft.Json;

namespace Dneprokos.Movies.Api.Client.Models.Movies
{
    public class MovieApiModel : IdNameBaseApiModel
    {
        [JsonProperty("release_date")]
        public int? ReleaseDate { get; set; }

        [JsonProperty("genre_ids")]
        public List<int?>? GenreIds { get; set; }
    }
}
