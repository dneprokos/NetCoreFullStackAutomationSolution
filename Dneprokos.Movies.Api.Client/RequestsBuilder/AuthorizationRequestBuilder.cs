using Dneprokos.Api.Base.Client.Core;
using Dneprokos.Movies.Api.Client.Data;
using Flurl;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder
{
    public class AuthorizationRequestBuilder : BaseApiClient
    {
        private string SearchUrl;

        public AuthorizationRequestBuilder(string baseUrl, ILogger logger) 
            : base(baseUrl, logger)
        {
            SearchUrl = MoviesEndpointsUrls.Authorization();
        }

        public AuthorizationRequestBuilder WithQueryUserName(string userName)
        {
            SearchUrl = SearchUrl.SetQueryParam("username", userName);
            return this;
        }

        public AuthorizationRequestBuilder WithQueryPassword(string password) 
        {
            SearchUrl = SearchUrl.SetQueryParam("password", password);
            return this;
        }

        public RestResponse SendPostRequest()
        {
            return UsePostMethod(SearchUrl)
                .AddHeader("accept", "application/json")
                .SendRequest();
        }

    }
}
