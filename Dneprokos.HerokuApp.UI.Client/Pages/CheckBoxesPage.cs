using Dneprokos.UI.Base.Client.TestBaseClasses;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class CheckBoxesPage : FrameworkBasePage
    {
        public static CheckBoxesPage Instance => new();

        #region Selectors

        private const string CheckBox1CheckBoxXPathSelector = "//form/input[1]";

        private const string CheckBox2CheckBoxXPathSelector = "//form/input[2]";

        #endregion

        #region Page Elements

        public CheckBoxWebElement CheckBox1CheckBox()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.XPath(CheckBox1CheckBoxXPathSelector)));

        public CheckBoxWebElement CheckBox2CheckBox()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.XPath(CheckBox2CheckBoxXPathSelector)));
        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to checkboxes page and returns new instance of <see cref="CheckBoxesPage"/>
        /// </summary>
        /// <returns></returns>
        public CheckBoxesPage NavigateToCheckBoxesPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/checkboxes");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Checks the CheckBox1 button
        /// </summary>
        /// <returns></returns>
        public CheckBoxesPage CheckCheckBox1()
        {
            CheckBox1CheckBox().Check();
            return this;
        }

        /// <summary>
        /// Checks the CheckBox2 button
        /// </summary>
        /// <returns></returns>
        public CheckBoxesPage CheckCheckBox2()
        {
            CheckBox2CheckBox().Check();
            return this;
        }

        /// <summary>
        /// Unchecks the CheckBox1 button
        /// </summary>
        /// <returns></returns>
        public CheckBoxesPage UnCheckCheckBox1()
        {
            CheckBox1CheckBox().UnCheck();
            return this;
        }

        /// <summary>
        /// Unchecks the CheckBox2 button
        /// </summary>
        /// <returns></returns>
        public CheckBoxesPage UnCheckCheckBox2()
        {
            CheckBox2CheckBox().UnCheck();
            return this;
        }

        /// <summary>
        /// Checks if CheckBox1 is checked
        /// </summary>
        /// <returns></returns>
        public bool IsCheckBox1Checked()
        {
            return CheckBox1CheckBox().IsChecked();
        }

        /// <summary>
        /// Checks if CheckBox2 is checked
        /// </summary>
        /// <returns></returns>
        public bool IsCheckBox2Checked()
        {
            return CheckBox2CheckBox().IsChecked();
        }

        #endregion
    }
}
