using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;
using NUnit.Framework;
using Dneprokos.UI.Base.Client.Constants;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class FileDownloadPage : FrameworkBasePage
    {
        public static FileDownloadPage Instance => new();

        #region Selectors

        private const string FirstLinkXPathSelector = "//div[@class='example']//a[1]";

        #endregion

        #region Page Elements

        public LinkWebElement FirstLink()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.XPath(FirstLinkXPathSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to checkboxes page and returns new instance of <see cref="FileDownloadPage"/>
        /// </summary>
        /// <returns></returns>
        public FileDownloadPage NavigateToFileDownloadPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/download");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Downloads first link on the page
        /// </summary>
        /// <returns></returns>
        public FileDownloadPage DownloadFistLink()
        {
            FirstLink().Click();
            return this;
        }

        /// <summary>
        /// Gets the file name of the first link on the page
        /// </summary>
        /// <returns></returns>
        public string GetFileNameOfFirstLink()
        {
            return FirstLink().GetText();
        }

        /// <summary>
        /// Wait until file is downloaded
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string WaitForFileDownloaded(string fileName)
        {
            string downloadPath = SeleniumConstants.DefaultDownloadDirectory();
            var filePath = Path.Combine(downloadPath, fileName);
            ConcurrentDriverManager.CurrentDriver.WaitForFileDownloaded(filePath);

            return filePath;
        }

        #endregion
    }
}
