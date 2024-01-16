using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class DragAndDropPage : FrameworkBasePage
    {
        public static DragAndDropPage Instance => new();

        #region Selectors

        private const string SquareADivIdSelector = "column-a";

        private const string SquareBDivIdSelector = "column-b";

        #endregion

        #region Page Elements

        public BaseWebElement SquareADiv()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.Id(SquareADivIdSelector)));

        public BaseWebElement SquareBDiv()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsClickable(By.Id(SquareBDivIdSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to drag and drop page and returns new instance of <see cref="DragAndDropPage"/>
        /// </summary>
        /// <returns></returns>
        public DragAndDropPage NavigateToDragAndDropPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/drag_and_drop");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Drags and drops square A to square B
        /// </summary>
        /// <returns></returns>
        public DragAndDropPage DragAndDropSquareAtoSquareB()
        {
            SquareADiv().JavaScriptDragAndDrop(SquareBDiv());
            return this;
        }

        /// <summary>
        /// Gets the text of square B
        /// </summary>
        /// <returns></returns>
        public string GetSquareBText()
        {
            return SquareBDiv().Element.Text;
        }

        #endregion
    }
}
