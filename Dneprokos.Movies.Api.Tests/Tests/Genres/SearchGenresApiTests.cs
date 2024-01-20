using Dneprokos.Api.Base.Client.Extenstions;
using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.Models._BaseModels;
using Dneprokos.Movies.Api.Client.Models.Genres;
using Dneprokos.Movies.Api.Tests.AssertionHelpers;
using Dneprokos.Movies.Api.Tests.BaseClasses;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using System.Net;

namespace Dneprokos.Movies.Api.Tests.Tests.Genres
{
    [NonParallelizable]
    public class SearchGenresApiTests : MoviesApiTestBase
    {
        [Test]
        public void SearcgGenres_PartialName_ShouldBeFound()
        {
            //Arrange
            var searchCriteria = "Film";

            //Act
            PaginationApiModel<GenreApiModel> genresResponse = MoviesApiRequests!
                .Genres()
                .WithQueryName(searchCriteria)
                .SendGetQueryRequest(RegularAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.OK)
                .ConvertJsonToModel<PaginationApiModel<GenreApiModel>>()!;

            //Assert
            using (new AssertionScope())
            {
                List<GenreApiModel> data = genresResponse.Data!;
                data.ForEach(record => record.Name.Should().Contain(searchCriteria));

                genresResponse.VerifyPaginationResponsePropsHaveDefaultValues();
                genresResponse.TotalResults.Should().Be(data.Count);
            }
        }

        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void SearcgGenres_WithValidPageNumberAndLimit_ShouldShownWithPagination(int pageNumber, int pageLimit)
        {
            //Arrange

            //Act
            PaginationApiModel<GenreApiModel> genresResponse = MoviesApiRequests!
                .Genres()
                .WithQueryPage(pageNumber)
                .WithQueryLimit(pageLimit)
                .SendGetQueryRequest(RegularAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.OK)
                .ConvertJsonToModel<PaginationApiModel<GenreApiModel>>()!;

            //Assert
            using (new AssertionScope())
            {
                genresResponse.PageNumber.Should().Be(pageNumber);
                genresResponse.PageLimit.Should().Be(pageLimit);
                genresResponse.Data!.Should().HaveCount(pageLimit);
            }
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void SearcgGenres_WithPageNumberLessThanMin_ShouldBeBadRequest(int pageNumber)
        {
            //Arrange

            //Act
            string content = MoviesApiRequests!
                .Genres()
                .WithQueryPage(pageNumber)
                .SendGetQueryRequest(RegularAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.BadRequest)
                .Content!;

            //Assert
            content.Should().Be(MoviesApiConstants.PageLessThanMinMessage);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void SearcgGenres_WithPageLimitLessThanMin_ShouldBeBadRequest(int pageLimit)
        {
            //Arrange

            //Act
            string content = MoviesApiRequests!
                .Genres()
                .WithQueryLimit(pageLimit)
                .SendGetQueryRequest(RegularAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.BadRequest)
                .Content!;

            //Assert
            content.Should().Be(MoviesApiConstants.LimitLessThanMinMessage);
        }
    }
}
