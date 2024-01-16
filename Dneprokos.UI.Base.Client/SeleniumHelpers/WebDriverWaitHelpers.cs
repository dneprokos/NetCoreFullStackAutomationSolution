using NUnit.Framework;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.Constants;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    public static class WebDriverWaitHelpers
    {
        /// <summary>
        /// Waits for the page to be loaded.
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="maxWaitSeconds">Max time to wait in seconds</param>
        public static void WaitForPageToLoad(
            this IWebDriver driver,
            int maxWaitSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitSeconds));
            wait.Until(webDriver => webDriver.RunJavaScript("return document.readyState").Equals("complete"));

            //TODO: Add more errors processings
            var possibleErrors = new List<string> { "500 Internal Server Error", "404 Not Found" };

            possibleErrors.ForEach(possibleError =>
            {
                if (driver.Title.Contains(possibleError))
                {
                    Assert.Fail($"Page contains an error: {driver.Title}");
                }
            });
        }

        /// <summary>
        /// Waits for the page to be loaded.
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="expectedErrors">List of error you want to verify in Page Title. E.g '500 Internal Server Error'</param>
        /// <param name="maxWaitSeconds">Max time to wait in seconds</param>
        public static void WaitForPageToLoad(
            this IWebDriver driver,
            List<string> expectedErrors,
            int maxWaitSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitSeconds));
            wait.Until(webDriver => webDriver.RunJavaScript("return document.readyState").Equals("complete"));

            expectedErrors.ForEach(possibleError =>
            {
                if (driver.Title.Contains(possibleError))
                {
                    Assert.Fail($"Page contains an error: {driver.Title}");
                }
            });
        }

        /// <summary>
        /// Waits for the network resources to be loaded.
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="maxWaitSeconds">Max Timeout to wait</param>
        public static void WaitForNetworkResourcesLoaded(
            this IWebDriver driver,
            int maxWaitSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(maxWaitSeconds));
            wait.Until(webDriver => webDriver.RunJavaScript("return window.performance.timing.loadEventEnd > 0;"));
        }

        /// <summary>
        /// Wait until URL equals the given URL
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="url">Expected URL</param>
        /// <param name="maxWaitSeconds">Max Timeout to wait</param>
        /// <returns></returns>
        public static bool WaitUntilUrlEquals(
            this IWebDriver driver,
            string url,
            int maxWaitSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                bool isUrlEqual = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitSeconds))
                    .Until(ExpectedConditions.UrlToBe(url));
                return isUrlEqual;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait until URL contains the given URL
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="expectedTitle">Expected URL part</param>
        /// <param name="maxWaitSeconds">Max Timeout to wait</param>
        /// <returns></returns>
        public static bool WaitUntilUrlContains(
            this IWebDriver driver,
            string expectedTitle,
            int maxWaitSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                bool isUrlContains = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitSeconds))
                    .Until(ExpectedConditions.UrlContains(expectedTitle));
                return isUrlContains;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait until title equals the given title
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="expectedTitle">Expected title</param>
        /// <param name="maxWaitSeconds">Max Timeout to wait</param>
        /// <returns></returns>
        public static bool WaitUntilTitleEquals(
            this IWebDriver driver,
            string expectedTitle,
            int maxWaitSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                bool isTitlePresent = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitSeconds))
                    .Until(ExpectedConditions.TitleIs(expectedTitle));
                return isTitlePresent;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait until title contains the given title
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="expectedTitlePart">Expected title part</param>
        /// <param name="maxWaitSeconds">Max Timeout to wait</param>
        /// <returns></returns>
        public static bool WaitUntilTitleContains(
            this IWebDriver driver,
            string expectedTitlePart,
            int maxWaitSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                bool isTitlePresent = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitSeconds))
                    .Until(ExpectedConditions.TitleContains(expectedTitlePart));
                return isTitlePresent;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Waits for the element to be visible
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="secondsTimeOut">Max time to wait in seconds</param>
        /// <returns></returns>
        public static IWebElement WaitForElement(
            this IWebDriver driver,
            By locator,
            int secondsTimeOut = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            if (secondsTimeOut <= 0)
                return driver.FindElement(locator);

            var wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(secondsTimeOut));

            try
            {
                return wait.Until(dr => driver.FindElement(locator));
            }
            catch (WebDriverTimeoutException exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        /// <summary>
        /// Waits for the elements to be visible
        /// </summary>
        /// <param name="driver">This WebDriver instance</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="secondsTimeOut">Max time to wait in seconds</param>
        /// <returns></returns>
        public static IReadOnlyCollection<IWebElement> WaitForElements(
            this IWebDriver driver,
            By locator,
            int secondsTimeOut = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            if (secondsTimeOut <= 0)
                return driver.FindElements(locator);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsTimeOut));

            try
            {
                return wait.Until(dr => dr.FindElements(locator));
            }
            catch (WebDriverTimeoutException exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        /// <summary>
        /// Waits for the element to be invisible
        /// </summary>
        /// <param name="driver">This WebDriver instance</param>
        /// <param name="locater">The locator by which to find the element</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns></returns>
        public static string WaitForElementNotVisible(
            this IWebDriver driver,
            By locater,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            if (driver == null || locater == null)
            {

                return "WebDriver or Locator is null";
            }
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.InvisibilityOfElementLocated(locater));
                return null!;
            }
            catch (TimeoutException)
            {
                return $"Element still visible after {timeOutInSeconds} seconds";
            }
        }

        /// <summary>
        /// Waits for the element to be clickable
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns></returns>
        public static IWebElement WaitUntilElementIsClickable(
            this IWebDriver driver,
            By locator,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                IWebElement webElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.ElementToBeClickable(locator));
                return webElement;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Waits for the element to be visible
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns>IWebElement</returns>
        public static IWebElement WaitUntilElementIsVisible(
            this IWebDriver driver,
            By locator,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                IWebElement webElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                .Until(ExpectedConditions.ElementIsVisible(locator));
                return webElement;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait for the element to exist
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="parentLocator">The locator by which to find the element</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns>IWebElement</returns>
        public static IWebElement WaitUntilElementExists(
            this IWebDriver driver,
            By parentLocator,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                IWebElement webElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.ElementExists(parentLocator));
                return webElement;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait until the element value has text
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="text">Element text</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns></returns>
        public static bool WaitUntilElementValueHasText(
            this IWebDriver driver,
            By locator,
            string text,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                bool isElementValueHasText = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.TextToBePresentInElementValue(locator, text));
                return isElementValueHasText;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait for the element to be invisible
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns></returns>
        public static bool WaitUntilElementInvisible(
            this IWebDriver driver,
            By locator,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                bool isVisible = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
                return isVisible;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait for the element with text to be invisible
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="text">Element text</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns></returns>
        public static bool WaitUntilElementWithTextIsInvisible(
            this IWebDriver driver,
            By locator,
            string text,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                bool isVisible = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.InvisibilityOfElementWithText(locator, text));
                return isVisible;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Waits for the frame to be available
        /// </summary>
        /// <param name="driver">This WebDriver instance</param>
        /// <param name="frameId"></param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns></returns>
        public static string WaitUntilFrameAvailableToSwitch(
            this IWebDriver driver,
            string frameId,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameId));
                return null!;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait for IFrame to be available and switch to it
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns></returns>
        public static IWebDriver WaitUntilFrameAvailableToSwitch(
            this IWebDriver driver,
            By locator,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                IWebDriver webElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));
                return webElement;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait for alert to be present
        /// </summary>
        /// <param name="driver">WebDriver instance for extension</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns>IAlert</returns>
        public static IAlert WaitUntilAlertIsPresent(
            this IWebDriver driver,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.AlertIsPresent());
                return alert;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Wait for specific condition to be true
        /// </summary>
        /// <param name="webDriver">WebDriver instance for extension</param>
        /// <param name="condition">Condition</param>
        /// <param name="timeoutInSeconds">Max time to wait in seconds</param>
        public static void WaitForCondition(
            this IWebDriver webDriver,
            Func<IWebDriver, bool> condition,
            int timeoutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(condition);
        }

        /// <summary>
        /// Wait for element using query selector. Can be used when element is in Shadow root
        /// </summary>
        /// <param name="driver">Current WebDriver</param>
        /// <param name="querySelector">Query selector.
        /// Example: "document.querySelector('[containername=email-composer-binder]').childNodes[1].shadowRoot.querySelector('[data-test-id=btn-send]')"</param>
        /// <param name="timeOutInSeconds">Max time to wait in seconds</param>
        /// <returns></returns>
        public static IWebElement WaitForElementUsingQuerySelector(
            this IWebDriver driver,
            string querySelector,
            int timeOutInSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            try
            {
                Func<IWebDriver, IWebElement> condition = (currentDriver) =>
                    (IWebElement)((IJavaScriptExecutor)currentDriver).ExecuteScript($"return {querySelector}");

                return new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds)).Until(condition);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
