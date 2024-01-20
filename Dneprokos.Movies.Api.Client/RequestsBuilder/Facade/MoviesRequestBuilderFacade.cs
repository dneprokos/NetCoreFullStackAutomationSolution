using Microsoft.Extensions.Logging;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder.Facade
{
    public class MoviesRequestBuilderFacade
    {
        private readonly string _baseUrl;
        private readonly ILogger? _logger;

        public MoviesRequestBuilderFacade(string baseUrl, ILogger? logger = null)
        {
            this._baseUrl = baseUrl;
            this._logger = logger;
        }

        public AuthorizationRequestBuilder Authorization()
            => new(this._baseUrl, this._logger!);

        public UsersRequestBuilder Users()
            => new(this._baseUrl, this._logger!);

        public GenresRequestBuilder Genres()
            => new(this._baseUrl, this._logger!);

        public MoviesRequestBuilder Movies()
            => new(this._baseUrl, this._logger!);
    }
}
