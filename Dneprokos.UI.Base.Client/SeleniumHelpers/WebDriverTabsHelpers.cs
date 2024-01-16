using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    /// <summary>
    /// Class to help with Browser tabs
    /// </summary>
    public static class WebDriverTabsHelpers
    {
        /// <summary>
        /// Switches to the last opened window
        /// </summary>
        /// <param name="webDriver"></param>
        public static void SwitchToNewWindow(this IWebDriver webDriver)
        {
            webDriver.SwitchTo().Window(webDriver.WindowHandles[webDriver.WindowHandles.Count - 1]);
        }

        /// <summary>
        /// Opens a new tab or window
        /// </summary>
        /// <param name="driver">This WebDriver instance</param>
        /// <param name="windowType"></param>
        public static void OpenNewTabOrWindow(this IWebDriver driver, WindowType windowType)
        {
            driver.SwitchTo().NewWindow(windowType);
        }
    }
}
