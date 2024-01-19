using Microsoft.Extensions.Logging;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder.Facade
{
    public class MoviesRequestBuilderFacade
    {
        private string baseUrl;
        private ILogger? _logger;

        public MoviesRequestBuilderFacade(string baseUrl, ILogger? logger = null)
        {
            this.baseUrl = baseUrl;
            this._logger = logger;
        }

        public AuthorizationRequestBuilder Authorization()
            => new(this.baseUrl, this._logger!);

        public UsersRequestBuilder Users()
            => new(this.baseUrl, this._logger!);

    }
}
