using Allure.Net.Commons;
using Dneprokos.Api.Base.Client.Extenstions;
using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.Models.Genres;
using Dneprokos.Movies.Api.Client.RequestActions;
using Dneprokos.Movies.Api.Tests.BaseClasses;
using Dneprokos.Movies.Api.Tests.Utils;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using System.Net;

namespace Dneprokos.Movies.Api.Tests.Tests.Genres
{
    [Parallelizable]
    [AllureSuite("GENRES")]
    [AllureSubSuite("DELETE /genres/{id}")]
    public class DeleteGenresApiTests : MoviesApiTestBase
    {
        [Test]
        [AllureTag(AllureTags.MainFlow)]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureIssue("ISSUE-29", "https://github.com/dneprokos/node-rest-services/issues/29")]
        public void DeleteGenre_WithAdmin_IdExist_ShouldBeDeleted()
        {
            //Arrange
            GenreApiModel newGenre = GenreActions.CreateRandomGenre(AdminAuthentication!, Logger!, BaseUrl!);

            //Act
            MoviesApiRequests!
                .Genres()
                .SendDeleteGenre(newGenre.Id!.Value, AdminAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.NoContent);

            //Assert
            GenreActions
                .GetGenreById(AdminAuthentication!, Logger!, BaseUrl!, newGenre.Id.Value)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.NotFound);
        }

        [Test]
        [AllureTag(AllureTags.MainFlow)]
        [AllureSeverity(SeverityLevel.normal)]
        public void DeleteGenre_WithAdmin_IdDoesNotExist_ShouldBeNotFound()
        {
            //Arrange
            var idDoesNotExit = int.MaxValue;

            //Act
            string response = MoviesApiRequests!
                .Genres()
                .SendDeleteGenre(idDoesNotExit, AdminAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.NotFound)
                .Content!;

            //Assert
            response.Should().Be(GenresConstants.GenreNotFoundMessage);
        }

        [Test]
        [AllureTag(AllureTags.MainFlow)]
        [AllureSeverity(SeverityLevel.normal)]
        public void DeleteGenre_WithNonAdmin_ShouldBeForbidden()
        {
            //Arrange

            //Act
            string response = MoviesApiRequests!
                .Genres()
                .SendDeleteGenre(1, RegularAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.Forbidden)
                .Content!;

            //Assert
            response.Should().Be(MoviesApiConstants.NoUserPermissionsMessage);
        }
    }
}
