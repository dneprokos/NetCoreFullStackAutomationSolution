using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using Dneprokos.UI.Base.Client.Constants;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    public static class WebDriverFilesHelper
    {
        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="driver">Driver.</param>
        /// <param name="webElement">WebElement</param>
        /// <param name="fileName">File name to be upload</param>
        public static void UploadFile(this IWebDriver driver, IWebElement webElement, string fileName)
        {
            webElement.Should().NotBeNull("WebElement should not be null");
            fileName.Should().NotBeNullOrEmpty("File name should not be null or empty");

            if (driver is IAllowsFileDetection allowsDetection)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }

            webElement.SendKeys(fileName);
        }

        /// <summary>
        /// Waits until file is downloaded to specific location
        /// </summary>
        /// <param name="driver">Current driver</param>
        /// <param name="filePath">Expected file location</param>
        /// <param name="timeoutSeconds">Timeout in second. Default: 30 seconds</param>
        public static void WaitForFileDownloaded(this IWebDriver driver, 
            string filePath, int timeoutSeconds = SeleniumConstants.DefaultWaitTimeInSeconds)
        {
            var timeOut = TimeSpan.FromSeconds(timeoutSeconds);

            var wait = new WebDriverWait(driver, timeOut);
            wait.Until(drv =>
            {
                try
                {
                    return File.Exists(filePath);
                }
                catch (IOException)
                {
                    // The file is still being written to, so we catch the exception and return false
                    return false;
                }
            });
        }
    }
}
