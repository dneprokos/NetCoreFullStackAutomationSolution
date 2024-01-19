using Dneprokos.Movies.Api.Client.RequestsBuilder.Facade;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RestSharp.Authenticators;

namespace Dneprokos.Movies.Api.Tests.BaseClasses
{
    [TestFixture]
    public class MoviesApiTestBase
    {
        public static string? BaseUrl;
        public static string? AdminUserName;
        public static string? AdminPassword;
        public static string? RegularUserName;
        public static string? RegularPassword;

        public static IAuthenticator? AdminAuthentication;
        public static IAuthenticator? RegularAuthentication;

        public static ILogger? Logger;

        public MoviesRequestBuilderFacade? MoviesApiRequests; 

        [OneTimeSetUp]
        public virtual void BeforeFeature()
        {
            BaseUrl = GlobalFixtureSetup.BaseUrl;
            AdminUserName = GlobalFixtureSetup.AdminUserName;
            AdminPassword = GlobalFixtureSetup.AdminPassword;
            RegularUserName = GlobalFixtureSetup.RegularUserName;
            RegularPassword = GlobalFixtureSetup.RegularPassword;

            Logger = GlobalFixtureSetup.Logger;

            MoviesApiRequests = new MoviesRequestBuilderFacade(BaseUrl!, Logger);

            AdminAuthentication = GlobalFixtureSetup.AdminAuthentication!;
            RegularAuthentication = GlobalFixtureSetup.RegularAuthentication!;
        }
    }
}
