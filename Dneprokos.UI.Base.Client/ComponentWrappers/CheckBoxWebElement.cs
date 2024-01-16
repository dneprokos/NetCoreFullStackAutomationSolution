using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.ComponentWrappers
{
    /// <summary>
    /// CheckBox web element.
    /// </summary>
    public class CheckBoxWebElement : ButtonWebElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        public CheckBoxWebElement(IWebElement webElement)
            : base(webElement, nameof(CheckBoxWebElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        public CheckBoxWebElement(IWebElement webElement, ILogger? logger)
            : base(webElement, logger, nameof(CheckBoxWebElement))
        {
        }

        /// <summary>
        /// Is the checkbox checked?
        /// </summary>
        /// <returns></returns>
        public bool IsChecked()
        {
            Logger?.LogInformation("Is checked?");
            return Element.Selected;
        }

        /// <summary>
        /// Checks the checkbox if it is not already checked.
        /// </summary>
        public void Check()
        {
            Logger?.LogInformation("Check element if not checked");
            if (!IsChecked())
                Element.Click();
        }

        /// <summary>
        /// Unchecks the checkbox if it is not already unchecked.
        /// </summary>
        public void UnCheck()
        {
            Logger?.LogInformation("Uncheck element if not unchecked");
            if (IsChecked())
                Element.Click();
        }
    }
}
