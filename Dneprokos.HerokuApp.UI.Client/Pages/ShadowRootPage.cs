using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class ShadowRootPage : FrameworkBasePage
    {
        public static ShadowRootPage Instance => new();

        #region Selectors

        private const string BoxReadonlyTextQuerySelector = "document.querySelector(\"#content > my-paragraph:nth-child(5) > ul\")";

        #endregion

        #region Page Elements

        public ReadonlyTextWebElement BoxReadonlyText()
            => new(ConcurrentDriverManager
                .CurrentDriver
                .WaitForElementUsingQuerySelector(BoxReadonlyTextQuerySelector));

        #endregion

        #region Action methods

        /// <summary>
        /// Navigates to shadow root page and returns new instance of <see cref="ShadowRootPage"/>
        /// </summary>
        /// <returns></returns>
        public ShadowRootPage NavigateToShadowRootPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/shadowdom");
            WaitForPageToLoad();
            return this;
        }

        public string GetBoxText()
        {
            return BoxReadonlyText().GetText();
        }

        #endregion
    }
}
