using Dneprokos.Movies.Api.Client.RequestsBuilder;
using Dneprokos.Movies.Api.Tests.BaseClasses;
using NUnit.Framework;
using RestSharp;

namespace Dneprokos.Movies.Api.Tests.Tests.Users
{
    [TestFixture]
    public class GetUsersApiTests : MoviesApiTestBase
    {
        [Test]
        public void GetUsers_WithAdminToken_ShouldBeReturned()
        {
            //Arrange
            RestResponse response = new AuthorizationRequestBuilder(BaseUrl!, Logger!)
                .WithQueryUserName(AdminUserName!)
                .WithQueryPassword(AdminPassword!)
                .SendPostRequest();

            //Act

            //Assert
        }
    }
}
