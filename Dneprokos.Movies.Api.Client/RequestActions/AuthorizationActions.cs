using Dneprokos.Api.Base.Client.Extenstions;
using Dneprokos.Movies.Api.Client.Models.Authorization;
using Dneprokos.Movies.Api.Client.RequestsBuilder;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Dneprokos.Movies.Api.Client.RequestActions
{
    public class AuthorizationActions
    {
        public static string? GenerateToken(
            string baseUrl, 
            ILogger logger,
            AuthorizationRequestParams authorizationParams)
        {
            AuthorizationApiModel? authorizationResponse = new AuthorizationRequestBuilder(baseUrl, logger)
                .WithQueryAuthorizationParams(authorizationParams)
                .SendPostRequest()
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.OK)
                .ConvertJsonToModel<AuthorizationApiModel>();

            authorizationResponse!.Should().NotBeNull();
            authorizationResponse!.AccessToken.Should().NotBeNullOrEmpty();

            return authorizationResponse!.AccessToken;
        }
    }
}
