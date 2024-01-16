using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using FluentAssertions;

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
    }
}
