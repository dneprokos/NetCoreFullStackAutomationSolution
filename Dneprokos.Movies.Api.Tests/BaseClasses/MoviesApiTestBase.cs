using Microsoft.Extensions.Logging;
using NUnit.Framework;

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

        public static ILogger? Logger;

        [OneTimeSetUp]
        public virtual void BeforeFeature()
        {
            BaseUrl = GlobalFixtureSetup.BaseUrl;
            AdminUserName = GlobalFixtureSetup.AdminUserName;
            AdminPassword = GlobalFixtureSetup.AdminPassword;
            RegularUserName = GlobalFixtureSetup.RegularUserName;
            RegularPassword = GlobalFixtureSetup.RegularPassword;

            Logger = GlobalFixtureSetup.Logger;
        }
    }
}
