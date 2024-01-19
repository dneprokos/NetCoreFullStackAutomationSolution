using Dneprokos.Helper.Base.Client.Configuration;
using Dneprokos.Helper.Base.Client.Loggers.Managers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Dneprokos.Movies.Api.Tests
{
    [SetUpFixture]
    public class GlobalFixtureSetup
    {
        public static string? BaseUrl;
        public static string? AdminUserName;
        public static string? AdminPassword;
        public static string? RegularUserName;
        public static string? RegularPassword;

        public static ILogger? Logger;

        [OneTimeSetUp]
        public void BeforeAllFeaturesGlobal()
        {
            BaseUrl = RunSettingsHelper.GetNotNullStringSetting("baseUrl");
            AdminUserName = RunSettingsHelper.GetNotNullStringSetting("adminUserName");
            AdminPassword = RunSettingsHelper.GetNotNullStringSetting("adminPassword");
            RegularUserName = RunSettingsHelper.GetNotNullStringSetting("regularUserName");
            RegularPassword = RunSettingsHelper.GetNotNullStringSetting("regularPassword");

            Logger = NLogLogger.Instance.Logger;
        }
    }
}
