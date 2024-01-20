using Dneprokos.Api.Base.Client.Extenstions;
using Dneprokos.Helper.Base.Client.RandomGenerators;
using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.Models.Genres;
using Dneprokos.Movies.Api.Client.RequestActions;
using Dneprokos.Movies.Api.Tests.BaseClasses;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Dneprokos.Movies.Api.Tests.Tests.Genres
{
    [Parallelizable]
    public class CreateGenreApiTests : MoviesApiTestBase
    {
        private List<int> _idsToCleanUp = new List<int>();


        [OneTimeTearDown]
        public void AfterFeature()
        {
            _idsToCleanUp.ForEach(id 
                => GenreActions.DeleteGenreSuccessfully(AdminAuthentication!, Logger!, BaseUrl!, id));
        }

        [Test]
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
