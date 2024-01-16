using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class DynamicLoadingPage : FrameworkBasePage
    {
        public static DynamicLoadingPage Instance => new();

        #region Selectors

        private const string StartButtonXPathSelector = "//button";

        private const string FinishReadonlyTextXPathSelector = "//div[@id='finish']//h4";

        #endregion

        #region Page Elements

        public ButtonWebElement StartButton()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.XPath(StartButtonXPathSelector)));

        public ReadonlyTextWebElement FinishText()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.XPath(FinishReadonlyTextXPathSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to dinamic loading page and returns new instance of <see cref="DynamicLoadingPage"/>
        /// </summary>
        /// <returns></returns>
        public DynamicLoadingPage NavigateToDynamicLoadingPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/dynamic_loading/1");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Clicks on the Start button
        /// </summary>
        /// <returns></returns>
        public DynamicLoadingPage ClickStart()
        {
            StartButton().Click();
            return this;
        }

        /// <summary>
        /// Gets the finish text
        /// </summary>
        /// <returns></returns>
        public string GetFinishText()
        {
            return FinishText().GetText();
        }

        #endregion
    }
}
