using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class MultipleWindowsPage : FrameworkBasePage
    {
        public static MultipleWindowsPage Instance => new();

        #region Selectors

        private const string ClickHereButtonLinkTextSelector = "Click Here";

        private const string NewWindowReadonlyTextXPathSelector = "//h3";

        #endregion

        #region Page Elements

        public ButtonWebElement ClickHereButton()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.LinkText(ClickHereButtonLinkTextSelector)));

        public ReadonlyTextWebElement NewWindowText()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.XPath(NewWindowReadonlyTextXPathSelector)));
        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to multiple windows page and returns new instance of <see cref="MultipleWindowsPage"/>
        /// </summary>
        /// <returns></returns>
        public MultipleWindowsPage NavigateToMultipleWindowsPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/windows");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Clicks on the Click Here button
        /// </summary>
        /// <returns></returns>
        public MultipleWindowsPage ClickHere()
        {
            ClickHereButton().Click();
            return this;
        }

        public MultipleWindowsPage SwitchToNewWindow()
        {
            ConcurrentDriverManager
                .CurrentDriver
                .SwitchTo()
                .Window(ConcurrentDriverManager
                .CurrentDriver.WindowHandles[1]);
            return this;
        }

        public MultipleWindowsPage SwitchToOriginalWindow()
        {
            ConcurrentDriverManager
                .CurrentDriver
                .SwitchTo()
                .Window(ConcurrentDriverManager
                .CurrentDriver.WindowHandles[0]);
            return this;
        }

        public string GetNewWindowText()
        {
            return NewWindowText().GetText();
        }

        #endregion
    }
}
