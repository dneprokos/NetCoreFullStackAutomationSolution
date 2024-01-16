using Dneprokos.UI.Base.Client.WebDriverCore;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

namespace Dneprokos.UI.Base.Client.ComponentWrappers
{
    /// <summary>
    /// Button web element.
    /// </summary>
    public class ButtonWebElement : ReadonlyTextWebElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        public ButtonWebElement(IWebElement webElement)
            : base(webElement, nameof(ButtonWebElement))
        {
        }

        /// <summary>
        /// initializes a new instance of the <see cref="ButtonWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="concreteElement">Element name for logger</param>
        public ButtonWebElement(IWebElement webElement, string concreteElement)
            : base(webElement, concreteElement)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        public ButtonWebElement(IWebElement webElement, ILogger? logger)
            : base(webElement, logger, nameof(ButtonWebElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger"></param>
        /// <param name="concreteElement">Element name for logger</param>
        public ButtonWebElement(IWebElement webElement, ILogger? logger, string concreteElement)
            : base(webElement, logger, concreteElement)
        {
        }

        /// <summary>
        /// Clicks the button.
        /// </summary>
        public void Click()
        {
            Logger?.LogInformation($"Clicking button");
            Element.Click();
        }

        /// <summary>
        /// Clicks the button using JavaScript.
        /// </summary>
        public void JavaScriptClick()
        {
            Logger?.LogInformation($"Clicking button");
            ConcurrentDriverManager.CurrentDriver.ExecuteJavaScript($"arguments[0].click();", Element);
        }

        /// <summary>
        /// Double clicks the button.
        /// </summary>
        public void DoubleClick()
        {
            Logger?.LogInformation($"Double clicking button");
            new Actions(ConcurrentDriverManager.CurrentDriver)
                .DoubleClick(Element)
                .Perform();
        }

        /// <summary>
        /// Double clicks the button.
        /// </summary>
        /// <param name="webDriver"></param>
        public void DoubleClick(IWebDriver webDriver)
        {
            Logger?.LogInformation($"Double clicking button");
            new Actions(webDriver)
                .DoubleClick(Element)
                .Perform();
        }

        /// <summary>
        /// Submits the button.
        /// </summary>
        public void Submit()
        {
            Logger?.LogInformation($"Submitting button");
            Element.Submit();
        }
    }
}
