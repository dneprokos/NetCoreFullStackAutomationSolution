using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.WebDriverCore;
using Dneprokos.UI.Base.Client.ComponentWrappers;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class HoversPage : FrameworkBasePage
    {
        public static HoversPage Instance => new();

        #region Selectors

        private const string FigureDivXPathSelector = "//div[@class='figure'][1]";

        private const string NameReadonlyTextXPathSelector = "//div[@class='figure'][1]//h5";

        #endregion

        #region Page Elements

        public IWebElement FigureDiv()
            => ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.XPath(FigureDivXPathSelector));

        public ReadonlyTextWebElement NameReadonlyText()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.XPath(NameReadonlyTextXPathSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to alerts page and returns new instance of <see cref="HoversPage"/>
        /// </summary>
        /// <returns></returns>
        public HoversPage NavigateToHoversPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/hovers");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Hovers over the figure
        /// </summary>
        /// <returns></returns>
        public HoversPage HoverOverFigure()
        {
            FigureDiv().HoverMouseOverElement();
            return this;
        }

        /// <summary>
        /// Gets the name text
        /// </summary>
        /// <returns></returns>
        public string GetNameText()
        {
            return NameReadonlyText().GetText();
        }

        #endregion
    }
}
