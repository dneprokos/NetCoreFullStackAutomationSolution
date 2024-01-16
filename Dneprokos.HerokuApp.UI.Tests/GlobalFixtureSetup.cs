using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions;
using NUnit.Framework;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Testcontainers.WebDriver;
using Dneprokos.UI.Base.Client.WebDriverCore;
using Dneprokos.Helper.Base.Client.Loggers.Managers;
using Dneprokos.UI.Base.Client.Loggers;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Common;

namespace Dneprokos.HerokuApp.UI.Tests
{
    [SetUpFixture]
    public class GlobalFixtureSetup
    {
        public static BrowserOptions? CurrentBrowserOptions;
        /// <summary>
        /// Docker container for the HerokuApp web application and WebDriver
        /// </summary>
        public static IContainer? WebApplicationContainer;

        public static WebDriverContainer? WebDriverContainer;

        /// <summary>
        /// URL of the application that running in docker
        /// </summary>
        public static string? DockerizedApplicationUrl { get; private set; }

        /// <summary>
        /// Url to HubUrl, like SeleniumGrid, Moon, BrowserStack and etc
        /// </summary>
        public static string? HubUrl { get; private set; }

        [OneTimeSetUp]
        public async Task BeforeAllTestFixturesAsync()
        {
            InternalLogger.Logger = NLogLogger.Instance!.Logger;

            CurrentBrowserOptions = BrowserOptionsHelper.GetWebDriverOptionsUsingRunSettings();
            if (CurrentBrowserOptions.IsRemote)
            {
                await StartHerokuAppContainer();
            }
        }

        [OneTimeTearDown]
        public async Task AfterAllTestFixtures()
        {
            new ConcurrentDriverManager().QuitAllDrivers();
            if (CurrentBrowserOptions!.IsRemote)
            {
                await StopHerokuAppContainer();
            }
        }

        #region Private helpers

        private static async Task StartHerokuAppContainer()
        {
            var hostName = "heroku-app-container";
            var imageName = "gprestes/the-internet";
            int internalPort = 5000;
            WebDriverBrowser containerBrowser;

            // Resolve WebDriver container browser
            switch (CurrentBrowserOptions!.Browser)
            {
                case SupportedBrowsers.Chrome:
                    containerBrowser = WebDriverBrowser.Chrome;
                    break;
                case SupportedBrowsers.Firefox:
                    containerBrowser = WebDriverBrowser.Firefox;
                    break;
                default: 
                    containerBrowser = WebDriverBrowser.Chrome;
                    break;
            }

            // Build application url
            Uri _webApplicationBaseAddress = new UriBuilder(
                Uri.UriSchemeHttp, hostName, internalPort).Uri;

            // Build WebDriver container
            WebDriverContainer = new WebDriverBuilder()
                .WithBrowser(containerBrowser)
                .Build();

            // Build docker container
            WebApplicationContainer = new ContainerBuilder()
                .WithImage(imageName)
                .WithNetwork(WebDriverContainer!.GetNetwork())
                .WithNetworkAliases(_webApplicationBaseAddress.Host)
                .WithPortBinding(7080, internalPort)
                .Build();

            // Start application and webdriver containers
            await WebApplicationContainer.StartAsync();
            await WebDriverContainer.StartAsync();

            // Save URL's
            DockerizedApplicationUrl = _webApplicationBaseAddress.ToString();
            HubUrl = WebDriverContainer.GetConnectionString();
        }

        private static async Task StopHerokuAppContainer()
        {
            await WebApplicationContainer!.DisposeAsync().AsTask();
            await WebDriverContainer!.DisposeAsync().AsTask();
        }

        #endregion
    }
}
