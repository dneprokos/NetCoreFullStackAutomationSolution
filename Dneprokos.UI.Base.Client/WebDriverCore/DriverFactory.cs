using Dneprokos.UI.Base.Client.Loggers;
using FluentAssertions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Chrome;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Common;
using Microsoft.Extensions.Logging;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions;

namespace Dneprokos.UI.Base.Client.WebDriverCore
{
    public class DriverFactory
    {
        private static ILogger Log = InternalLogger.Logger;

        protected DriverFactory()
        {
        }

        /// <summary>
        /// Creates a new web driver based on the provided options.
        /// </summary>
        /// <param name="browserOptions"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static IWebDriver CreateWebDriver(BrowserOptions browserOptions)
        {
            Log.LogInformation($"---------Starting WebDriver Initialization---------------");
            Log.LogInformation($"'{browserOptions.Browser}' browser driver will be created");
            browserOptions.LogBrowserOptionsDebugLevel(Log);

            IWebDriver driver = browserOptions.Browser switch
            {
                SupportedBrowsers.Chrome => CreateChromeDriver(browserOptions),
                SupportedBrowsers.Firefox => CreateFirefoxDriver(browserOptions),
                _ => throw new NotSupportedException($"Browser {browserOptions.Browser} is not supported"),
            };
            Log.LogInformation($"----------WebDriver was Initialized-----------------------");
            return driver;
        }

        #region Private Methods

        private static IWebDriver CreateChromeDriver(BrowserOptions driverOptions)
        {
            //--- Add support for Chrome options
            var options = new ChromeOptions();

            // --- Headless mode resolving
            if (driverOptions.IsHeadless)
                options.AddArgument("--headless");

            // --- Disable info bars resolving
            if (driverOptions.DisableInfoBars)
                options.AddArgument("--disable-infobars");

            // --- Set the window size if provided in the driverOptions
            if (driverOptions.ScreenResolution != null)
                options.AddArgument($"--window-size={driverOptions.ScreenResolution.Width},{driverOptions.ScreenResolution.Height}");
            else options.AddArgument("--start-maximized");

            // --- Resolve chrome specific options
            if (driverOptions.ChromeSpecific != null)
            {
                ChromeWebDriverOptions chromeSpecific = driverOptions.ChromeSpecific;

                // Resolve device emulation
                if (chromeSpecific.DeviceEmulation != null)
                {
                    if (chromeSpecific?.DeviceEmulation?.DeviceSettings == null)
                    {
                        options.EnableMobileEmulation(chromeSpecific?.DeviceEmulation?.DeviceName);
                    }
                    else
                    {
                        options.EnableMobileEmulation(chromeSpecific?.DeviceEmulation?.DeviceSettings);
                    }
                }
            }

            // --- Instantiate the driver
            if (driverOptions.IsRemote)
                return CreateRemoteWebDriver(options, driverOptions.HubUri!);
            else return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver(BrowserOptions driverOptions)
        {
            //----Add support for Firefox options
            var options = new FirefoxOptions();

            // Headless mode resolving
            if (driverOptions.IsHeadless)
                options.AddArgument("--headless");

            // --- Disable info bars resolving
            if (driverOptions.DisableInfoBars)
                options.AddArgument("--disable-infobars");

            // Set the window size if provided in the driverOptions
            if (driverOptions.ScreenResolution != null)
            {
                options.AddArgument($"--width={driverOptions.ScreenResolution.Width}");
                options.AddArgument($"--height={driverOptions.ScreenResolution.Height}");
            }
            else options.AddArgument("--start-maximized");

            // --- Instantiate the driver
            if (driverOptions.IsRemote)
                return CreateRemoteWebDriver(options, driverOptions.HubUri!);
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateRemoteWebDriver(DriverOptions driverOptions, string hubUri)
        {
            driverOptions.Should().NotBeNull("Driver options are required for remote driver");
            hubUri.Should().NotBeNull("HubUri is required for remote driver");

            Log.LogInformation($"Remote driver will be initialized with Hub URL: {hubUri}");
            return new RemoteWebDriver(new Uri(hubUri), driverOptions);
        }

        #endregion
    }
}
