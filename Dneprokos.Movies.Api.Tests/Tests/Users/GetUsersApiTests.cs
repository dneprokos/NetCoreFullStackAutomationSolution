using Dneprokos.Movies.Api.Tests.BaseClasses;
using NUnit.Framework;
using Dneprokos.Api.Base.Client.Extenstions;
using System.Net;
using Dneprokos.Movies.Api.Client.Models.Users;
using FluentAssertions;
using Dneprokos.Movies.Api.Client.Data;

namespace Dneprokos.Movies.Api.Tests.Tests.Users
{
    [TestFixture]
    public class GetUsersApiTests : MoviesApiTestBase
    {
        [Test]
        public void GetUsers_WithAdminToken_ShouldBeReturned()
        {
            //Arrange
            var expectedUsers = new List<UserApiModel>
            {
                new UserApiModel {Id = 1, Username = "testadmin", Role = "admin"},
                new UserApiModel {Id = 2, Username = "test", Role = "member"},
            };

            //Act
            List<UserApiModel> responseUsers = MoviesApiRequests!
                .Users()
                .SendGetUsersRequest(AdminAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.OK)
                .ConvertJsonToModel<List<UserApiModel>>()!;

            //Assert
            responseUsers.Should().BeEquivalentTo(expectedUsers, p => p.Excluding(p => p.Password));
        }

        [Test]
        public void GetUsers_WithRegularToken_ShouldBeForbidden()
        {
            //Arrange

            //Act
            string responseContent = MoviesApiRequests!
                .Users()
                .SendGetUsersRequest(RegularAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.Forbidden)
                .Content!;

            //Assert
            responseContent.Should().Be(MoviesApiConstants.NoUserPermissionsMessage);
        }
    }
}
