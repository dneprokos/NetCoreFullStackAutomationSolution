using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.Models.Authorization;
using Dneprokos.Movies.Api.Client.RequestsBuilder.Base;
using Flurl;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder
{
    public class AuthorizationRequestBuilder : MoviesRequestBuilderBase
    {
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

        public AuthorizationRequestBuilder WithQueryAuthorizationParams(AuthorizationRequestParams queryParams) 
        {
            SearchUrl = SearchUrl.SetQueryParam("username", queryParams.UserName);
            SearchUrl = SearchUrl.SetQueryParam("password", queryParams.Password);
            return this;
        }

        public RestResponse SendPostRequest()
        {
            return UsePostMethod(SearchUrl!)
                .AddHeader("accept", "application/json")
                .SendRequest();
        }

    }
}
