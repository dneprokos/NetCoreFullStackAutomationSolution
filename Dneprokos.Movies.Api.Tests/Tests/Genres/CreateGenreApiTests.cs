using Allure.Net.Commons;
using Dneprokos.Api.Base.Client.Extenstions;
using Dneprokos.Helper.Base.Client.RandomGenerators;
using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.Models.Genres;
using Dneprokos.Movies.Api.Client.RequestActions;
using Dneprokos.Movies.Api.Tests.BaseClasses;
using Dneprokos.Movies.Api.Tests.Utils;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using System.Net;

namespace Dneprokos.Movies.Api.Tests.Tests.Genres
{
    [Parallelizable]
    [AllureSuite("GENRES")]
    [AllureSubSuite("POST /genres")]
    public class CreateGenreApiTests : MoviesApiTestBase
    {
        private List<int> _idsToCleanUp = new();

        [OneTimeTearDown]
        public void AfterFeature()
        {
            _idsToCleanUp.ForEach(id 
                => GenreActions.DeleteGenreSuccessfully(AdminAuthentication!, Logger!, BaseUrl!, id));
        }

        [Test]
        [AllureTag(AllureTags.MainFlow)]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateGenre_WithValidNameLength_ShouldBeCreated()
        {
            //Arrange
            var newGenreName = StringGenerator.GenerateRandomString(3);

            //Act
            GenreApiModel responseBody = MoviesApiRequests!
                .Genres()
                .WithBodyName(newGenreName)
                .SendPostGenres(AdminAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.Created)
                .ConvertJsonToModel<GenreApiModel>()!;

            //Assert
            using (new AssertionScope())
            {
                responseBody.Name.Should().Be(newGenreName);
                responseBody.Id.Should().BeGreaterThan(0);
            }
        }

        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        public void CreateGenre_WithNameLessThanMinSymbols_ShouldBeBadRequest()
        {
            //Arrange
            var newGenreName = StringGenerator.GenerateRandomString(2);

            //Act
            string responseContent = MoviesApiRequests!
                .Genres()
                .WithBodyName(newGenreName)
                .SendPostGenres(AdminAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.BadRequest)
                .Content!;

            //Assert
            responseContent.Should().Be(MoviesApiConstants.NameLessSymbolsMesssage);
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateGenre_WithNonAdminToken_ShouldBeForbidden()
        {
            //Arrange
            var newGenreName = StringGenerator.GenerateRandomString(3);

            //Act
            string responseContent = MoviesApiRequests!
                .Genres()
                .WithBodyName(newGenreName)
                .SendPostGenres(RegularAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.Forbidden)
                .Content!;

            //Assert
            responseContent.Should().Be(MoviesApiConstants.NoUserPermissionsMessage);
        }
    }
}
