using Dneprokos.Helper.Base.Client.Configuration;
using Dneprokos.Helper.Base.Client.Loggers.Managers;
using Dneprokos.Movies.Api.Client.Models.Authorization;
using Dneprokos.Movies.Api.Client.RequestActions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RestSharp.Authenticators;

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

        public static IAuthenticator? AdminAuthentication;
        public static IAuthenticator? RegularAuthentication;

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

            string adminToken = AuthorizationActions
                .GenerateToken(BaseUrl, Logger, new AuthorizationRequestParams(AdminUserName, AdminPassword))!;
            string regularToken = AuthorizationActions
                .GenerateToken(BaseUrl, Logger, new AuthorizationRequestParams(RegularUserName, RegularPassword))!;

            AdminAuthentication = new JwtAuthenticator(adminToken);
            RegularAuthentication = new JwtAuthenticator(regularToken);
        }
    }
}
