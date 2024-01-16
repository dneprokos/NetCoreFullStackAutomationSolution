using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Dneprokos.UI.Base.Client.WebDriverCore;
using Dneprokos.UI.Base.Client.Constants;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    public static class WebElementHelpers
    {
        /// <summary>
        /// Hover mouse over the element
        /// </summary>
        /// <param name="element">WebElement for extension</param>
        public static void HoverMouseOverElement(this IWebElement element)
        {
            var action = new Actions(ConcurrentDriverManager.CurrentDriver);
            action.MoveToElement(element).Perform();
        }

        /// <summary>
        /// Waits for the element to be visible
        /// </summary>
        /// <param name="element">WebElement for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="secondsTimeOut">Max time to wait in seconds</param>
        /// <returns></returns>
        public static IWebElement WaitForElement(
            this IWebElement element,
            By locator,
            int secondsTimeOut = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            if (secondsTimeOut <= 0)
                return element.FindElement(locator);

            var wait = new WebDriverWait(
                ConcurrentDriverManager.CurrentDriver,
                TimeSpan.FromSeconds(secondsTimeOut));

            try
            {
                return wait.Until(dr => element.FindElement(locator));
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
        /// <param name="element">WebElement for extension</param>
        /// <param name="locator">The locator by which to find the element</param>
        /// <param name="secondsTimeOut">Max time to wait in seconds</param>
        /// <returns></returns>
        public static IReadOnlyCollection<IWebElement> WaitForElements(
            this IWebElement element,
            By locator,
            int secondsTimeOut = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            if (secondsTimeOut <= 0)
                return element.FindElements(locator);

            var wait = new WebDriverWait(
                ConcurrentDriverManager.CurrentDriver,
                TimeSpan.FromSeconds(secondsTimeOut));

            try
            {
                return wait.Until(dr => element.FindElements(locator));
            }
            catch (WebDriverTimeoutException exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }
    }
}
