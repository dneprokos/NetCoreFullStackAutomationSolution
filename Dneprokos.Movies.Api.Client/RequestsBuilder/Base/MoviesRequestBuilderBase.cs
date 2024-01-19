using Dneprokos.Api.Base.Client.Core;
using Microsoft.Extensions.Logging;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder.Base
{
    public class MoviesRequestBuilderBase : BaseApiClient
    {
        protected string? SearchUrl;

        public MoviesRequestBuilderBase(string baseUrl, ILogger logger)
            : base(baseUrl, logger)
        {
        }
    }
}
