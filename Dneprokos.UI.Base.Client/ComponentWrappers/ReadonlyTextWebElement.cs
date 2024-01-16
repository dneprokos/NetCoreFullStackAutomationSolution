using Dneprokos.UI.Base.Client.SeleniumHelpers;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.ComponentWrappers
{
    /// <summary>
    /// Represents a read-only text web element.
    /// </summary>
    public class ReadonlyTextWebElement : BaseWebElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadonlyTextWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        public ReadonlyTextWebElement(IWebElement webElement)
            : base(webElement, nameof(ReadonlyTextWebElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadonlyTextWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        public ReadonlyTextWebElement(IWebElement webElement, ILogger? logger)
            : base(webElement, logger!, nameof(ReadonlyTextWebElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadonlyTextWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="concreteElement">Element name for logger</param>
        public ReadonlyTextWebElement(IWebElement webElement, ILogger? logger, string concreteElement)
            : base(webElement, logger!, concreteElement)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadonlyTextWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="concreteElement">Element name for logger</param>
        public ReadonlyTextWebElement(IWebElement webElement, string concreteElement)
            : base(webElement, concreteElement)
        {
        }

        /// <summary>
        /// Gets the text of the web element.
        /// </summary>
        /// <returns></returns>
        public string GetText() => Element.Text;

        /// <summary>
        /// Gets the inner text of the web element.
        /// </summary>
        /// <returns></returns>
        public string? GetInnerText()
            => JavaScriptHelpers
            .RunJavaScript("return arguments[0].innerText; ", Element) as string;
    }
}
