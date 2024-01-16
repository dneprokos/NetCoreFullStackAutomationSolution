using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    public static class WebDriverAlertHelpers
    {
        /// <summary>
        /// Switches to Alert window
        /// </summary>
        /// <param name="webDriver">This WebDriver instance</param>
        /// <returns></returns>
        public static IAlert SwitchToAlert(this IWebDriver webDriver)
        {
            return webDriver.SwitchTo().Alert();
        }

        /// <summary>
        /// Switches to Alert window and accepts it
        /// </summary>
        /// <param name="webDriver"></param>
        public static void SwitchToAlertAndAccept(this IWebDriver webDriver)
        {
            webDriver.SwitchToAlert().Accept();
        }

        /// <summary>
        /// Switches to Alert window and dismisses it
        /// </summary>
        /// <param name="webDriver"></param>
        public static void SwitchToAlertAndDismiss(this IWebDriver webDriver)
        {
            webDriver.SwitchToAlert().Dismiss();
        }
    }
}
