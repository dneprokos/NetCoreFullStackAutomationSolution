using Dneprokos.Api.Base.Client.Extenstions;
using Dneprokos.Movies.Api.Client.RequestsBuilder;
using Microsoft.Extensions.Logging;
using RestSharp.Authenticators;
using System.Net;

namespace Dneprokos.Movies.Api.Client.RequestActions
{
    public class GenreActions
    {
        public static void DeleteGenreSuccessfully(
            IAuthenticator authorization, ILogger logger, string baseUrl, int genreId)
        {
            new GenresRequestBuilder(baseUrl, logger)
                .SendDeleteGenre(genreId, authorization)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.NoContent);
        } 
    }
}
