using FluentAssertions;
using OpenQA.Selenium.Chromium;

namespace Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Chrome
{
    public class MobileChromeEmulation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobileChromeEmulation"/> class with the specified device name.
        /// </summary>
        /// <param name="deviceName">Mobile device name</param>
        public MobileChromeEmulation(string deviceName)
        {
            deviceName.Should().NotBeNullOrEmpty("Device name is required");
            DeviceName = deviceName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileChromeEmulation"/> class with the specified device settings.
        /// </summary>
        /// <param name="deviceSettings">Mobile device settings</param>
        public MobileChromeEmulation(ChromiumMobileEmulationDeviceSettings deviceSettings)
        {
            deviceSettings.Should().NotBeNull("Device settings are required");
            DeviceSettings = deviceSettings;
        }

        /// <summary>
        /// Emulated device name. See <see cref="ChromiumMobileEmulationDeviceSettings"/> for supported devices.
        /// </summary>
        public string? DeviceName { get; private set; }

        /// <summary>
        /// Device settings. See <see cref="ChromiumMobileEmulationDeviceSettings"/> for supported devices.
        /// </summary>
        public ChromiumMobileEmulationDeviceSettings? DeviceSettings { get; private set; }
    }
}
