namespace Dneprokos.UI.Base.Client.Constants
{
    public class RunSettingsConstants
    {
        public const string Browser = "browser";

        /// <summary>
        /// Run settings key for isRemoteBrowser setting. Like Selenium Grid, Moon, etc.
        /// </summary>
        public const string IsRemoteBrowser = "isRemoteBrowser";

        /// <summary>
        /// Run settings key for browser mode setting. True/False 
        /// </summary>
        public const string IsHeadless = "isHeadless";

        /// <summary>
        /// Hub URL for remote browser. Like Selenium Grid, Moon, etc. Will require isRemoteBrowser to be set to true.
        /// </summary>
        public const string HubUrl = "hubUrl";

        /// <summary>
        /// Screen size for browser. Like 1920x1080, 1024x768, etc. Will maximize browser if not specified.
        /// </summary>
        public const string ScreenSize = "screenSize";

        /// <summary>
        /// Should disable infobars?
        /// </summary>
        public const string DisableInfoBars = "disableInfoBars";

        /// <summary>
        /// Name of the mobile device
        /// </summary>
        public const string MobileDeviceName = "mobileDeviceName";
    }
}
