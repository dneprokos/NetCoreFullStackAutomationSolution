using Dneprokos.UI.Base.Client.WebDriverCore;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Dneprokos.UI.Base.Client.TestBaseClasses
{
    [TestFixture]
    public abstract class ConcurrentDriverBaseTests
    {
        protected ILogger? Log;
        protected BrowserOptions? CurrentWebDriverOptions;

        [OneTimeSetUp]
        public virtual void BeforeTestFixture()
        {
            Log?.LogInformation($"Starting to run the test fixture: {TestContext.CurrentContext.Test.ClassName}");
            CurrentWebDriverOptions = BrowserOptionsHelper.GetWebDriverOptionsUsingRunSettings();
        }

        [OneTimeTearDown]
        public virtual void AfterTestFixture()
        {
            Log?.LogInformation($"----Finished running the test fixture: {TestContext.CurrentContext.Test.ClassName}---");
        }

        [SetUp]
        public virtual void BeforeTest()
        {
            Log?.LogInformation($"Starting running test: {TestContext.CurrentContext.Test.Name}");
            new ConcurrentDriverManager().StartTestDriver(CurrentWebDriverOptions!);
        }

        [TearDown]
        public virtual void AfterTest()
        {
            Log?.LogInformation($"Finished running test: {TestContext.CurrentContext.Test.Name}");
            new ConcurrentDriverManager().StopTestDriverAndRemoveFromPool();
        }
    }
}
