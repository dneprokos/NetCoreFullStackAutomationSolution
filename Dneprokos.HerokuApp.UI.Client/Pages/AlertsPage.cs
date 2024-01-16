using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.WebDriverCore;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.SeleniumHelpers;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class AlertsPage : FrameworkBasePage
    {
        public static AlertsPage Instance => new();

        #region Selectors

        private const string ClickForJSAlertButtonXPathSelector = "//button[text()='Click for JS Alert']";

        private const string ClickForJSConfirmButtonXPathSelector = "//button[text()='Click for JS Confirm']";

        private const string ClickForJSPromptButtonXPathSelector = "//button[text()='Click for JS Prompt']";

        private const string ResultReadonlyTextIdSelector = "result";

        #endregion

        #region Page Elements

        public ButtonWebElement ClickForJSAlertButton()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.XPath(ClickForJSAlertButtonXPathSelector)));

        public ButtonWebElement ClickForJSConfirmButton()
            => new(ConcurrentDriverManager.CurrentDriver
                .WaitUntilElementIsClickable(By.XPath(ClickForJSConfirmButtonXPathSelector)));

        public ButtonWebElement ClickForJSPromptButton()
            => new(ConcurrentDriverManager.CurrentDriver
                .WaitUntilElementIsClickable(By.XPath(ClickForJSPromptButtonXPathSelector)));

        public ReadonlyTextWebElement ResultReadonlyText()
            => new(ConcurrentDriverManager.CurrentDriver
                .WaitUntilElementIsVisible(By.Id(ResultReadonlyTextIdSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to alerts page and returns new instance of <see cref="AlertsPage"/>
        /// </summary>
        /// <returns></returns>
        public AlertsPage NavigateToAlertsPage(string baseUrl)
        {
            ConcurrentDriverManager.CurrentDriver.Navigate().GoToUrl(baseUrl + "/javascript_alerts");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Clicks on the Click for JS Alert button
        /// </summary>
        /// <returns></returns>
        public AlertsPage ClickForJSAlert()
        {
            ClickForJSAlertButton().Click();
            return this;
        }

        /// <summary>
        /// Clicks on the Click for JS Confirm button
        /// </summary>
        /// <returns></returns>
        public AlertsPage ClickForJSConfirm()
        {
            ClickForJSConfirmButton().Click();
            return this;
        }

        /// <summary>
        /// Clicks on the Click for JS Prompt button
        /// </summary>
        /// <returns></returns>
        public AlertsPage ClickForJSPrompt()
        {
            ClickForJSPromptButton().Click();
            return this;
        }

        /// <summary>
        /// Accepts the alert
        /// </summary>
        /// <returns></returns>
        public AlertsPage AcceptAlert()
        {
            ConcurrentDriverManager.CurrentDriver.SwitchTo().Alert().Accept();
            return this;
        }

        /// <summary>
        /// Dismisses the alert
        /// </summary>
        /// <returns></returns>
        public AlertsPage DismissAlert()
        {
            ConcurrentDriverManager.CurrentDriver.SwitchTo().Alert().Dismiss();
            return this;
        }

        /// <summary>
        /// Sends text to the alert
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <returns></returns>
        public AlertsPage SendTextToAlert(string text)
        {
            ConcurrentDriverManager.CurrentDriver.SwitchTo().Alert().SendKeys(text);
            return this;
        }

        /// <summary>
        /// Gets the alert text
        /// </summary>
        /// <returns></returns>
        public string GetAlertText()
        {
            return ConcurrentDriverManager.CurrentDriver.SwitchTo().Alert().Text;
        }

        /// <summary>
        /// Gets the result text
        /// </summary>
        /// <returns></returns>
        public string GetResultText()
        {
            return ResultReadonlyText().GetText();
        }

        #endregion
    }
}
