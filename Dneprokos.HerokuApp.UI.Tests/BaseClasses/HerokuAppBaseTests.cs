using Dneprokos.UI.Base.Client.TestBaseClasses;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Dneprokos.Helper.Base.Client.Configuration;
using Dneprokos.UI.Base.Client.WebDriverCore;
using Dneprokos.UI.Base.Client.SeleniumHelpers;

namespace Dneprokos.HerokuApp.UI.Tests.BaseClasses
{
    [TestFixture]
    public class HerokuAppBaseTests : ConcurrentDriverBaseTests
    {
        protected string? BaseUrl { get; set; }

        [OneTimeSetUp]
        public override void BeforeTestFixture()
        {
            Log?.LogInformation($"Starting to run the test fixture: {TestContext.CurrentContext.Test.ClassName}");
            CurrentWebDriverOptions = GlobalFixtureSetup.CurrentBrowserOptions!;

            if (CurrentWebDriverOptions.IsRemote)
            {
                Log?.LogInformation($"Hub Url: {GlobalFixtureSetup.HubUrl}");
                CurrentWebDriverOptions.HubUri = GlobalFixtureSetup.HubUrl;
                BaseUrl = GlobalFixtureSetup.DockerizedApplicationUrl;
            }
            else
            {
                BaseUrl = RunSettingsHelper.GetNotNullStringSetting("serverAppUrl");
            }
        }

        [TearDown]
        public override void AfterTest()
        {
            ConcurrentDriverManager.CurrentDriver?
                .MakeScreenShotOnFailure(TestContext.CurrentContext.Test.Name, true);
            base.AfterTest();
        }
    }
}
