using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    public static class WebDriverScrollHelpers
    {
        /// <summary>
        /// Performs a horizontal scroll if the page is wider than the viewport.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool PerformHorizontalScroll(this IWebDriver driver)
        {
            return (bool)driver.RunJavaScript("return document.documentElement.scrollWidth>document.documentElement.clientWidth;");
        }

        /// <summary>
        /// Performs a vertical scroll if the page is taller than the viewport.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool PerformVerticalScroll(this IWebDriver driver)
        {
            return (bool)driver.RunJavaScript("return document.documentElement.scrollHeight>document.documentElement.clientHeight;");
        }

        /// <summary>
        /// Performs a scroll into view of the given element.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void PerformScrollIntoWebElementView(this IWebDriver driver, IWebElement element)
        {
            driver.RunJavaScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        /// Performs a scroll into view of the given element top.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void PerformScrollIntoWebElementViewTop(this IWebDriver driver, IWebElement element)
        {
            driver.RunJavaScript("arguments[0].scrollIntoView(false);", element);
        }

        /// <summary>
        /// Performs a scroll to the top of the page.
        /// </summary>
        /// <param name="driver"></param>
        public static void PerformScrollToTop(this IWebDriver driver)
        {
            driver.RunJavaScript("scroll(0,0)");
            driver.RunJavaScript("window.scrollTo(0,0)");
        }

        /// <summary>
        /// Performs a scroll to the bottom of the page.
        /// </summary>
        /// <param name="driver"></param>
        public static void PerformScrollToBottom(this IWebDriver driver)
        {
            driver.RunJavaScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        /// <summary>
        /// Performs a horizontal scroll to the left.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="scrollSize"></param>
        public static void PerformHorizontalLeftScroll(this IWebDriver driver, int scrollSize)
        {
            driver.RunJavaScript("document.querySelector('.Table__scrollbar').scrollLeft -=" + scrollSize);
        }
    }
}
