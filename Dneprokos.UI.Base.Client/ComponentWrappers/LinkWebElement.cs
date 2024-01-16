using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.ComponentWrappers
{
    /// <summary>
    /// Href link web element
    /// </summary>
    public class LinkWebElement : ButtonWebElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        public LinkWebElement(IWebElement webElement)
            : base(webElement, nameof(LinkWebElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        public LinkWebElement(IWebElement webElement, ILogger? logger)
            : base(webElement, logger, nameof(LinkWebElement))
        {
        }

        /// <summary>
        /// Get the href attribute
        /// </summary>
        /// <returns></returns>
        public string GetHref()
        {
            return Element.GetAttribute("href");
        }
    }
}
