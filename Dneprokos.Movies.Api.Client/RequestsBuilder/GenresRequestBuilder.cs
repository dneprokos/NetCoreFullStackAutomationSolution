using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.Models.Genres;
using Dneprokos.Movies.Api.Client.RequestsBuilder.Base;
using Flurl;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder
{
    public class GenresRequestBuilder : MoviesRequestBuilderBase
    {
        private GenreApiModel _body;
        private List<GenreApiModel> _bulkBody;

        public GenresRequestBuilder(string baseUrl, ILogger logger) 
            : base(baseUrl, logger)
        {
            SearchUrl = MoviesEndpointsUrls.GenresSearch();
            _body = new GenreApiModel();
            _bulkBody = new List<GenreApiModel>();
        }

        #region Search query params

        public GenresRequestBuilder WithQueryName(string name)
        {
            SearchUrl = SearchUrl.SetQueryParam("name", name);
            return this;
        }

        public GenresRequestBuilder WithQueryPage(int page)
        {
            AddQueryPage(page);
            return this;
        }

        public GenresRequestBuilder WithQueryLimit(int limit)
        {
            AddQueryLimit(limit);
            return this;
        }

        #endregion

        #region Create/Update body

        public GenresRequestBuilder WithBodyName(string name)
        {
            _body.Name = name;
            return this;
        }

        #endregion

        #region Bulk create body

        public GenresRequestBuilder WithBulkBodyName(params string[] name)
        {
            name.ToList().ForEach(name 
                => _bulkBody.Add(new GenreApiModel { Name = name }));
            return this;
        }

        #endregion

        #region Send request

        public RestResponse SendGetAllGenres(IAuthenticator authenticator) 
        {
            return UseGetMethod(MoviesEndpointsUrls.Genres(), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .SendRequest();
        }

        public RestResponse SendPostGenres(IAuthenticator authenticator)
        {
            return UsePostMethod(MoviesEndpointsUrls.Genres(), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .AddHeader(DefaultContentType)
                .AddBody(_body)
                .SendRequest();
        }

        public RestResponse SendPostBulkGenre(IAuthenticator authenticator)
        {
            return UsePostMethod(MoviesEndpointsUrls.Genres(), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .AddHeader(DefaultContentType)
                .AddBody(_bulkBody)
                .SendRequest();
        }

        public RestResponse SendGetGenreById(int id, IAuthenticator authenticator)
        {
            return UseGetMethod(MoviesEndpointsUrls.Genres(id), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .SendRequest();
        }

        public RestResponse SendPutGenre(int id, IAuthenticator authenticator)
        {
            return UsePutMethod(MoviesEndpointsUrls.Genres(id), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .AddHeader(DefaultContentType)
                .AddBody(_body)
                .SendRequest();
        }

        public RestResponse SendDeleteGenre(int id, IAuthenticator authenticator)
        {
            return UseDeleteMethod(MoviesEndpointsUrls.Genres(id), authenticator)
                .AddHeader(DefaultAcceptHeader)
                .AddBody(_body)
                .SendRequest();
        }

        public RestResponse SendGetQueryRequest(IAuthenticator authenticator)
        {
            return UseGetMethod(SearchUrl!, authenticator)
                .AddHeader(DefaultAcceptHeader)
                .SendRequest();
        }

        #endregion
    }
}
