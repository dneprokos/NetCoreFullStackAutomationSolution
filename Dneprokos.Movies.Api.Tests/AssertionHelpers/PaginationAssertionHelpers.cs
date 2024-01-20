using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.Models._BaseModels;
using FluentAssertions;

namespace Dneprokos.Movies.Api.Tests.AssertionHelpers
{
    public static class PaginationAssertionHelpers
    {
        public static void VerifyPaginationResponsePropsHaveDefaultValues<T>(
            this PaginationApiModel<T> paginationResponse)
        {
            paginationResponse.PageNumber.Should().Be(MoviesApiConstants.DefautPageNumber);
            paginationResponse.PageLimit.Should().Be(MoviesApiConstants.DefautPageLimit);
        }
    }
}
