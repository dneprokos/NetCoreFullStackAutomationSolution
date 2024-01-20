using Dneprokos.Api.Base.Client.Core;
using Flurl;
using Microsoft.Extensions.Logging;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder.Base
{
    public class MoviesRequestBuilderBase : BaseApiClient
    {
        protected string? SearchUrl;

        public KeyValuePair<string, string> DefaultAcceptHeader = new("Accept", "application/json");
        public KeyValuePair<string, string> DefaultContentType = new("Content-Type", "application/json");

        public MoviesRequestBuilderBase(string baseUrl, ILogger logger)
            : base(baseUrl, logger)
        {
        }

        public void AddQueryPage(int page)
        {
            SearchUrl = SearchUrl.SetQueryParam("page", page);
        }

        public void AddQueryLimit(int limit)
        {
            SearchUrl = SearchUrl.SetQueryParam("limit", limit);
        }
    }
}
