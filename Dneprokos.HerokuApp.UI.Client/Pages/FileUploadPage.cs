using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class FileUploadPage : FrameworkBasePage
    {
        public static FileUploadPage Instance => new();

        #region Selectors

        private const string SelectFileInputIdSelector = "file-upload";

        private const string UploadButtonIdSelector = "file-submit";

        #endregion

        #region Page Elements

        public InputWebElement SelectFileInput()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.Id(SelectFileInputIdSelector)));

        public ButtonWebElement UploadButton()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.Id(UploadButtonIdSelector)));

        public ReadonlyTextWebElement UploadedFileName()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.Id("uploaded-files")));
        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to checkboxes page and returns new instance of <see cref="FileUploadPage"/>
        /// </summary>
        /// <returns></returns>
        public FileUploadPage NavigateToFileUploadPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/upload");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Uploads file
        /// </summary>
        /// <param name="filePath">Path to file to upload</param>
        /// <returns></returns>
        public FileUploadPage UploadFile(string filePath)
        {
            ConcurrentDriverManager
                .CurrentDriver.UploadFile(SelectFileInput().Element, filePath);
            UploadButton().Click();
            return this;
        }

        /// <summary>
        /// Gets uploaded file name
        /// </summary>
        /// <returns></returns>
        public string GetUploadedFileName()
        {
            return UploadedFileName().GetText();
        }

        #endregion
    }
}
