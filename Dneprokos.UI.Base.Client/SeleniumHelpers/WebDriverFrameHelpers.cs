using Dneprokos.UI.Base.Client.Constants;
using FluentAssertions;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    /// <summary>
    /// Class to help with WebDriver frame operations
    /// </summary>
    public static class WebDriverFrameHelpers
    {
        /// <summary>
        /// Enters into the Frame
        /// </summary>
        /// <param name="webDriver">This WebDriver instance</param>
        /// <param name="frameNameOrId"></param>
        /// <param name="secondsTimeOut"></param>
        public static void SwitchToFrame(this IWebDriver webDriver, string frameNameOrId, int secondsTimeOut = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            frameNameOrId.Should().NotBeNullOrEmpty("Frame name or id cannot be null or empty");
            webDriver.WaitUntilFrameAvailableToSwitch(frameNameOrId, secondsTimeOut);
        }

        /// <summary>
        /// Exists from Frame to default DOM content
        /// </summary>
        /// <param name="webDriver">This WebDriver instance</param>
        public static void SwitchToDefaultContent(this IWebDriver webDriver)
        {
            webDriver.SwitchTo().DefaultContent();
        }
    }
}
