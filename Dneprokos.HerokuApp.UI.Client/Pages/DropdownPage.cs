using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class DropdownPage : FrameworkBasePage
    {
        public static DropdownPage Instance => new();

        #region Selectors

        private const string DropdownListSelectIdSelector = "dropdown";

        #endregion

        #region Page Elements

        public SelectWebElement SelectDropdownList()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.Id(DropdownListSelectIdSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to dropdown page and returns new instance of <see cref="DropdownPage"/>
        /// </summary>
        /// <returns></returns>
        public DropdownPage NavigateToDropdownPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/dropdown");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Selects the option 1 from the dropdown list
        /// </summary>
        /// <returns></returns>
        public DropdownPage SelectOption1()
        {
            SelectDropdownList().SelectByText("Option 1");
            return this;
        }

        public string GetSelectedText()
        {
            return SelectDropdownList().GetSelectedText();
        }

        #endregion
    }
}
