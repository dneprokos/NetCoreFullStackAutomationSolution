using Dneprokos.Api.Base.Client.Extenstions;
using Dneprokos.Helper.Base.Client.RandomGenerators;
using Dneprokos.Movies.Api.Client.Models.Genres;
using Dneprokos.Movies.Api.Client.RequestsBuilder;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace Dneprokos.Movies.Api.Client.RequestActions
{
    public class GenreActions
    {
        /// <summary>
        /// Deletes a genres and verifies if status code is successfull
        /// </summary>
        /// <param name="authorization">Authorization</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="baseUrl">Base Url</param>
        /// <param name="genreId">Genre Id</param>
        public static void DeleteGenreSuccessfully(
            IAuthenticator authorization, ILogger logger, string baseUrl, int genreId)
        {
            new GenresRequestBuilder(baseUrl, logger)
                .SendDeleteGenre(genreId, authorization)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Get genre by id and return response
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="logger"></param>
        /// <param name="baseUrl"></param>
        /// <param name="genreId"></param>
        /// <returns></returns>
        public static RestResponse GetGenreById(IAuthenticator authorization, ILogger logger, string baseUrl, int genreId)
        {
            return new GenresRequestBuilder(baseUrl, logger).SendGetGenreById(genreId, authorization);
        }

        /// <summary>
        /// Creates new genre, verifies if status code is successfull and return created object
        /// </summary>
        /// <param name="authorization">Authorization</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="baseUrl">Base Url</param>
        /// <returns></returns>
        public static GenreApiModel CreateRandomGenre(IAuthenticator authorization, ILogger logger, string baseUrl)
        {
            var newGenreName = StringGenerator.GenerateRandomString(3);

            return new GenresRequestBuilder(baseUrl, logger)
                .WithBodyName(newGenreName)
                .SendPostGenres(authorization)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.Created)
                .ConvertJsonToModel<GenreApiModel>()!;
        }
    }
}
