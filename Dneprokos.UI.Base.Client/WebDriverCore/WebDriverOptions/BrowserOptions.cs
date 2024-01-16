using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Chrome;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Common;

namespace Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions
{
    /// <summary>
    /// WebDriver options. Used to configure the driver.
    /// </summary>
    public class BrowserOptions
    {
        /// <summary>
        /// Size of the browser window. See <see cref="ScreenResolutionOptions"/>. Contains Width and Height. Else will start maximized
        /// </summary>
        public ScreenResolutionOptions? ScreenResolution { get; set; }

        /// <summary>
        /// Type of the browser to be used for the test. See <see cref="SupportedBrowsers"/> for supported browsers.
        /// </summary>
        public SupportedBrowsers Browser { get; set; }

        /// <summary>
        /// Indicates if the driver should run in headless mode. Supported only for Chrome and Firefox.
        /// </summary>
        public bool IsHeadless { get; set; }

        /// <summary>
        /// Indicates if the driver is local or remote('isRemoteBrowser' settings). Set to true if you want to run your tests using some hub like Selenium Grid and etc.
        /// </summary>
        public bool IsRemote { get; set; }

        /// <summary>
        /// Required if IsRemote is true. The URL of the Selenium Grid or Moon Hub or Zalenium.
        /// </summary>
        public string? HubUri { get; set; }

        public bool DisableInfoBars { get; set; }

        /// <summary>
        /// Options specific to Chrome browser and not supported by other browsers.
        /// </summary>
        public ChromeWebDriverOptions? ChromeSpecific { get; set; }
    }
}
