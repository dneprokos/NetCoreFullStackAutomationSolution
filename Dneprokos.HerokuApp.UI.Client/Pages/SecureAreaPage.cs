using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class SecureAreaPage : FrameworkBasePage
    {
        public static SecureAreaPage Instance => new();

        #region Selectors

        private const string LogoutButtonPartialLinkTextSelector = "Logout";

        #endregion

        #region Page Elements

        public ButtonWebElement LogoutButton()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.PartialLinkText(LogoutButtonPartialLinkTextSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Logs out of the application
        /// </summary>
        /// <returns></returns>
        public LoginPage Logout()
        {
            LogoutButton().Click();
            WaitForPageToLoad();
            return new LoginPage();
        }

        #endregion
    }
}