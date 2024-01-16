namespace Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Chrome
{
    /// <summary>
    /// Options specific to Chrome browser and not supported by other browsers.
    /// </summary>
    public class ChromeWebDriverOptions
    {
        /// <summary>
        /// Chrome device emulation settings. See <see cref="MobileChromeEmulation"/> for supported devices.
        /// </summary>
        public MobileChromeEmulation? DeviceEmulation { get; set; }
    }
}
