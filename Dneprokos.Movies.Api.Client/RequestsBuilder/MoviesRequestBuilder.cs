using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.RequestsBuilder.Base;
using Microsoft.Extensions.Logging;
using RestSharp.Authenticators;
using RestSharp;
using Dneprokos.Movies.Api.Client.Models.Movies;
using Flurl;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder
{
    public class MoviesRequestBuilder : MoviesRequestBuilderBase
    {
        private readonly MovieApiModel _body; 

        public MoviesRequestBuilder(string baseUrl, ILogger logger) 
            : base(baseUrl, logger)
        {
            SearchUrl = MoviesEndpointsUrls.Movies();
            _body = new MovieApiModel();
        }

        #region Search query params

        public MoviesRequestBuilder WithQueryName(string name)
        {
            SearchUrl = SearchUrl.SetQueryParam("name", name);
            return this;
        }

        public MoviesRequestBuilder WithQueryReleaseDate(int releaseYear)
        {
            SearchUrl = SearchUrl.SetQueryParam("release_date", releaseYear);
            return this;
        }

        public MoviesRequestBuilder WithQueryPage(int page)
        {
            AddQueryPage(page);
            return this;
        }

        public MoviesRequestBuilder WithQueryLimit(int limit)
        {
            AddQueryLimit(limit);
            return this;
        }

        #endregion

        #region Send requests

        public RestResponse SendGetGenreById(int id, IAuthenticator authenticator)
        {
            return UseGetMethod(MoviesEndpointsUrls.Movies(id), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .SendRequest();
        }

        // TODO : Delete movie
        public RestResponse SendGetDelete(int id, IAuthenticator authenticator)
        {
            return UseDeleteMethod(MoviesEndpointsUrls.Movies(id), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .SendRequest();
        }

        public RestResponse SendGetQueryRequest(IAuthenticator authenticator)
        {
            return UseGetMethod(SearchUrl!, authenticator)
                .AddHeader(DefaultAcceptHeader)
                .SendRequest();
        }

        // TODO: Create movie
        public RestResponse SendPostGenres(IAuthenticator authenticator)
        {
            return UsePostMethod(MoviesEndpointsUrls.Movies(), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .AddBody(_body)
                .SendRequest();
        }

        #endregion
    }
}
