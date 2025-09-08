using Dneprokos.Movies.Api.Client.Models.Movies;
using Dneprokos.Movies.Api.Tests.BaseClasses;
using Dneprokos.Api.Base.Client.Extenstions;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using System.Net;
using FluentAssertions;

namespace Dneprokos.Movies.Api.Tests.Tests.Movies
{
    [Parallelizable]
    [AllureSuite("MOVIES")]
    [AllureSubSuite("POST /movies")]
    public class CreateMovieApiTests : MoviesApiTestBase
    {
        [Test]
        public void CreateMovie_WithAllValidFields_ShouldBeCreated()
        {
            // Arrange
            MovieApiModel payload = new MovieApiModelBuilder()
                .WithId(1)
                .WithName("Back to the Future")
                .WithReleaseDate(1985)
                .AddGenreId(1)
                .Build();

            var payload2 = (MovieApiModel)payload.Clone();
            payload2.Name = "AImenator 2";


            // Act
            MovieApiModel resPayload = MoviesApiRequests!
                .Movies()
                .SendPost(AdminAuthentication!, payload)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.Created)
                .ConvertJsonToModel<MovieApiModel>()!;

            // Assert
            resPayload
                .Should()
                .BeEquivalentTo(payload, c => c.Excluding(p => p.Id));
        }
    }
}
