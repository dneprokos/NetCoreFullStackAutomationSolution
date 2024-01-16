using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.ComponentWrappers
{
    /// <summary>
    /// Represents an input element.
    /// </summary>
    public class InputWebElement : ReadonlyTextWebElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        public InputWebElement(IWebElement webElement)
            : base(webElement, nameof(InputWebElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        public InputWebElement(IWebElement webElement, ILogger? logger)
            : base(webElement, logger, nameof(InputWebElement))
        {
        }

        /// <summary>
        /// Sets the value of the input element.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            Logger?.LogInformation($"Setting value of input element to '{value}'");
            Element.SendKeys(value);
        }

        /// <summary>
        /// Clears the value of the input element.
        /// </summary>
        public void Clear()
        {
            Logger?.LogInformation("Clearing value of input element");
            Element.Clear();
        }

        /// <summary>
        /// Clears the value of the input element and sets the value.
        /// </summary>
        /// <param name="value"></param>
        public void ClearAndSetValue(string value)
        {
            Clear();
            SetValue(value);
        }

        /// <summary>
        /// Submits the form containing the input element.
        /// </summary>
        public void Submit()
        {
            Logger?.LogInformation("Submitting form containing input element");
            Element.Submit();
        }
    }
}
