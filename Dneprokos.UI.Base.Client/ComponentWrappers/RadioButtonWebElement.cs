using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.ComponentWrappers
{
    /// <summary>
    /// Represents a radio button on a web page.
    /// </summary>
    public class RadioButtonWebElement : BaseWebElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        public RadioButtonWebElement(IWebElement webElement)
            : base(webElement, nameof(RadioButtonWebElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        public RadioButtonWebElement(IWebElement webElement, ILogger? logger)
            : base(webElement, logger!, nameof(RadioButtonWebElement))
        {
        }

        /// <summary>
        /// Indicates whether the radio button is selected.
        /// </summary>
        /// <returns></returns>
        public bool IsSelected()
        {
            return Element.Selected;
        }

        /// <summary>
        /// Selects the radio button.
        /// </summary>
        public void Select()
        {
            if (!IsSelected())
                Element.Click();
        }

        /// <summary>
        /// Clicks the radio button.
        /// </summary>
        public void Click()
        {
            Element.Click();
        }
    }
}
