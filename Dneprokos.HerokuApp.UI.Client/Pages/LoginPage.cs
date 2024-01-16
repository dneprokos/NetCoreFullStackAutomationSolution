using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class LoginPage : FrameworkBasePage
    {
        public static LoginPage Instance => new();

        #region Selectors

        private const string UsernameInputIdSelector = "username";

        private const string PasswordInputIdSelector = "password";

        private const string LoginButtonClassSelector = "radius";

        #endregion

        #region Page Elements

        public InputWebElement UsernameInput()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.Id(UsernameInputIdSelector)));

        public InputWebElement UsernamePassword()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.Id(PasswordInputIdSelector)));

        public ButtonWebElement LoginButton()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.ClassName(LoginButtonClassSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to login page and returns new instance of <see cref="LoginPage"/>
        /// </summary>
        /// <returns></returns>
        public LoginPage NavigateToLoginPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/login");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Logs in to the application
        /// </summary>
        /// <param name="username">Username to login with</param>
        /// <param name="password">Password to login with</param>
        /// <returns></returns>
        public SecureAreaPage Login(string username, string password)
        {
            UsernameInput().ClearAndSetValue(username);
            UsernamePassword().ClearAndSetValue(password);
            LoginButton().Click();
            WaitForPageToLoad();
            return new SecureAreaPage();
        }

        #endregion
    }
}
