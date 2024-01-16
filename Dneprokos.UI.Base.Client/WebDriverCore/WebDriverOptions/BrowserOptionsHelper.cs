using Dneprokos.UI.Base.Client.Configuration;
using Dneprokos.UI.Base.Client.Constants;
using Dneprokos.UI.Base.Client.Loggers;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Chrome;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Common;
using Microsoft.Extensions.Logging;

namespace Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions
{
    public static class BrowserOptionsHelper
    {
        /// <summary>
        /// Resolves the WebDriver options using the .runsettings file.
        /// </summary>
        /// <returns>WebDriverOptions See <see cref="BrowserOptions"/></returns>
        public static BrowserOptions GetWebDriverOptionsUsingRunSettings()
        {
            InternalLogger.Logger.LogInformation("Resolving WebDriver options using .runsettings file.");

            SupportedBrowsers browser = ResolveBrowserUsingRunSettings();
            var options = new BrowserOptions
            {
                Browser = browser,
                ScreenResolution = ResolveScreenSizeUsingRunSettings(),
                IsRemote = RunSettingsHelper.GetNullableBooleanSetting(RunSettingsConstants.IsRemoteBrowser),
                IsHeadless = RunSettingsHelper.GetNullableBooleanSetting(RunSettingsConstants.IsHeadless),
            };
            options.HubUri = ResolveHubUrlUsingRunSettings();

            options.DisableInfoBars = RunSettingsHelper.GetNullableBooleanSetting(RunSettingsConstants.DisableInfoBars);
            options.ChromeSpecific = ResolveChromeSpecificOptionsUsingRunSettings(browser);

            return options;
        }

        #region Private Methods

        private static ChromeWebDriverOptions ResolveChromeSpecificOptionsUsingRunSettings(SupportedBrowsers browserType)
        {
            ChromeWebDriverOptions? chromeSepcificOptions = null;

            string? deviceName = RunSettingsHelper.GetNullAbleStringSetting(RunSettingsConstants.MobileDeviceName);
            if (browserType == SupportedBrowsers.Chrome && deviceName != null)
            {
                chromeSepcificOptions = new ChromeWebDriverOptions();

                var mobileEmulation = new MobileChromeEmulation(deviceName);
                chromeSepcificOptions.DeviceEmulation = mobileEmulation;
            }

            return chromeSepcificOptions!;
        }

        private static string? ResolveHubUrlUsingRunSettings()
        {
            return RunSettingsHelper.GetNullAbleStringSetting(RunSettingsConstants.HubUrl);
        }

        private static SupportedBrowsers ResolveBrowserUsingRunSettings()
        {
            string browser = RunSettingsHelper.GetNotNullStringSetting(RunSettingsConstants.Browser);

            return browser.ToLower() switch
            {
                "chrome" => SupportedBrowsers.Chrome,
                "firefox" => SupportedBrowsers.Firefox,
                _ => throw new Exception($"Browser '{browser}' is not supported"),
            };
        }

        private static ScreenResolutionOptions? ResolveScreenSizeUsingRunSettings()
        {
            string? screenSize = RunSettingsHelper.GetNullAbleStringSetting(RunSettingsConstants.ScreenSize);

            if (screenSize == null)
                return null;
            else
            {
                string[] dimensions = screenSize
                    .Split(
                        new[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                if (dimensions.Length == 2 && int.TryParse(dimensions[0], out int width) && int.TryParse(dimensions[1], out int height))
                    return new ScreenResolutionOptions(width, height);
                else
                    throw new InvalidOperationException($"Invalid '{RunSettingsConstants.ScreenSize}' value in .runsettings file. Ensure it is in 'width, height' format.");
            }
        }

        #endregion
    }
}
