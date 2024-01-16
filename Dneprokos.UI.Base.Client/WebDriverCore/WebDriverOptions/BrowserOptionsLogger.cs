using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Chrome;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Common;
using Microsoft.Extensions.Logging;

namespace Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions
{
    public static class BrowserOptionsLogger
    {
        /// <summary>
        /// Log the options at debug level
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public static void LogBrowserOptionsDebugLevel(this BrowserOptions options, ILogger? logger)
        {
            if (logger != null)
            {
                logger?.LogDebug("WebDriver options: ");
                logger?.LogDebug($"    Browser: {options.Browser}");
                logger?.LogDebug($"    Windows Size: {options.ScreenResolution}");
                logger?.LogDebug($"    Is Headless: {options.IsHeadless}");
                logger?.LogDebug($"    Is Remote: {options.IsRemote}");
                logger?.LogDebug($"    Disable Info Bars: {options.DisableInfoBars}");

                if (options.IsRemote)
                {
                    logger?.LogDebug($"    Hub Uri: {options.HubUri}");
                }

                if (options.Browser == SupportedBrowsers.Chrome && options.ChromeSpecific != null)
                {
                    logger?.LogDebug($"    Chrome Specific Options:");
                    ChromeWebDriverOptions chromeOptions = options.ChromeSpecific;
                    if (chromeOptions.DeviceEmulation != null)
                    {
                        MobileChromeEmulation chromeMobile = chromeOptions.DeviceEmulation;
                        logger?.LogDebug($"        Device Name: {chromeMobile.DeviceName}");
                    }
                }
            }
        }
    }
}
