using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using OpenQA.Selenium;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class FramesPage : FrameworkBasePage
    {
        public static FramesPage Instance => new();

        #region Selectors

        private const string ContentReadonlyTextIdSelector = "content";

        #endregion

        #region Page Elements

        public ReadonlyTextWebElement ContentReadonlyText()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitUntilElementIsVisible(By.Id(ContentReadonlyTextIdSelector)));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to frames page and returns new instance of <see cref="FramesPage"/>
        /// </summary>
        /// <returns></returns>
        public FramesPage NavigateToFramesPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/nested_frames");
            WaitForPageToLoad();
            return this;
        }

        /// <summary>
        /// Switches to the top frame
        /// </summary>
        /// <returns></returns>
        public FramesPage SwitchToTopFrame()
        {
            ConcurrentDriverManager
                .CurrentDriver
                .SwitchTo()
                .Frame("frame-top");
            return this;
        }

        /// <summary>
        /// Switches to the middle frame
        /// </summary>
        /// <returns></returns>
        public FramesPage SwitchToMiddleFrame()
        {
            ConcurrentDriverManager
                .CurrentDriver
                .SwitchTo()
                .Frame("frame-middle");
            return this;
        }

        /// <summary>
        /// Gets the content text
        /// </summary>
        /// <returns></returns>
        public string GetContentText()
        {
            return ContentReadonlyText().GetText();
        }

        #endregion
    }
}
