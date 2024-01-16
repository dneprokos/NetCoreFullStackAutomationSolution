using Dneprokos.UI.Base.Client.Loggers;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using Dneprokos.UI.Base.Client.WebDriverCore;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.TestBaseClasses
{
    public class FrameworkBasePage
    {
        public readonly ILogger? Log;

        public FrameworkBasePage()
        {
            Log = InternalLogger.Logger;
        }

        public FrameworkBasePage(ILogger log)
        {
            Log = log;
        }

        /// <summary>
        /// Waits for page to load with current test driver
        /// </summary>
        public void WaitForPageToLoad()
        {
            Log?.LogInformation("Waiting for page to load");
            ConcurrentDriverManager.CurrentDriver.WaitForPageToLoad();
        }

        /// <summary>
        /// Waits for page to load with specified driver
        /// </summary>
        /// <param name="webDriver"></param>
        public void WaitForPageToLoad(IWebDriver webDriver)
        {
            Log?.LogInformation("Waiting for page to load");
            webDriver.WaitForPageToLoad();
        }

        /// <summary>
        /// Gets current page title
        /// </summary>
        /// <returns></returns>
        public string GetPageTitle()
        {
            Log?.LogInformation("Getting page title");
            return ConcurrentDriverManager.CurrentDriver.Title;
        }

        /// <summary>
        /// Gets current page title with specified driver
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public string GetPageTitle(IWebDriver webDriver)
        {
            Log?.LogInformation("Getting page title");
            return webDriver.Title;
        }

        /// <summary>
        /// Gets current page source
        /// </summary>
        /// <returns></returns>
        public string GetPageSource()
        {
            Log?.LogInformation("Getting page source");
            return ConcurrentDriverManager.CurrentDriver.PageSource;
        }

        /// <summary>
        /// Gets current page source with specified driver
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public string GetPageSource(IWebDriver webDriver)
        {
            Log?.LogInformation("Getting page source");
            return webDriver.PageSource;
        }

        /// <summary>
        /// Gets current page url
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUrl()
        {
            Log?.LogInformation("Getting current url");
            return ConcurrentDriverManager.CurrentDriver.Url;
        }

        /// <summary>
        /// Gets current page url with specified driver
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public string GetCurrentUrl(IWebDriver webDriver)
        {
            Log?.LogInformation("Getting current url");
            return webDriver.Url;
        }

        /// <summary>
        /// Go to url with current test driver and wait for page to load if specified
        /// </summary>
        /// <param name="url"></param>
        /// <param name="waitForPageToLoad"></param>
        public void GoToUrl(string url, bool waitForPageToLoad)
        {
            Log?.LogInformation("Going to url: {0}", url);
            ConcurrentDriverManager.CurrentDriver.Navigate().GoToUrl(url);
            if (waitForPageToLoad)
            {
                WaitForPageToLoad();
            }
        }

        /// <summary>
        /// Go to url with current test driver and wait for page to load
        /// </summary>
        /// <param name="url"></param>
        public void GoToUrl(string url)
        {
            Log?.LogInformation("Going to url: {0}", url);
            GoToUrl(url, true);
        }

        /// <summary>
        /// Go to url with specified driver and wait for page to load if specified
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="url"></param>
        /// <param name="waitForPageToLoad"></param>
        public void GoToUrl(IWebDriver webDriver, string url, bool waitForPageToLoad)
        {
            Log?.LogInformation("Going to url: {0}", url);
            webDriver.Navigate().GoToUrl(url);
            if (waitForPageToLoad)
            {
                WaitForPageToLoad(webDriver);
            }
        }

        /// <summary>
        /// Go to url with specified driver and wait for page to load
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="url"></param>
        public void GoToUrl(IWebDriver webDriver, string url)
        {
            GoToUrl(webDriver, url, true);
        }

        /// <summary>
        /// Refresh page with current test driver
        /// </summary>
        public void RefreshPage()
        {
            Log?.LogInformation("Refreshing page");
            ConcurrentDriverManager.CurrentDriver.Navigate().Refresh();
        }

        /// <summary>
        /// Refresh page with specified driver
        /// </summary>
        /// <param name="webDriver"></param>
        public void RefreshPage(IWebDriver webDriver)
        {
            Log?.LogInformation("Refreshing page");
            webDriver.Navigate().Refresh();
        }

        /// <summary>
        /// Take screenshot with current test driver
        /// </summary>
        /// <param name="fileName"></param>
        public void TakeScreenShot(string fileName)
        {
            Log?.LogInformation("Taking screenshot with filename: {0}", fileName);
            WebDriverScreenShotHelpers.MakeScreenShot(ConcurrentDriverManager.CurrentDriver, fileName);
        }
    }
}
