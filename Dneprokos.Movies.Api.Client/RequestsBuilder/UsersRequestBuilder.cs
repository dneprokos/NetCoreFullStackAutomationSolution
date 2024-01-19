using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.RequestsBuilder.Base;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;

namespace Dneprokos.Movies.Api.Client.RequestsBuilder
{
    public class UsersRequestBuilder : MoviesRequestBuilderBase
    {
        public UsersRequestBuilder(string baseUrl, ILogger logger)
            : base(baseUrl, logger)
        { 
        }

        public RestResponse SendGetUsersRequest(IAuthenticator authenticator)
        {
            return UseGetMethod(MoviesEndpointsUrls.Users(), authenticator)
                .AddHeader("accept", "application/json")
                .SendRequest();
        }
    }
}
