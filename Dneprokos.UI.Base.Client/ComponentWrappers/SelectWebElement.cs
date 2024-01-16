using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Dneprokos.UI.Base.Client.ComponentWrappers
{
    /// <summary>
    /// SelectWebElement is a wrapper around the Selenium SelectElement class.
    /// </summary>
    public class SelectWebElement : BaseWebElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        public SelectWebElement(IWebElement webElement)
            : base(webElement, nameof(SelectWebElement))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        public SelectWebElement(IWebElement webElement, ILogger? logger)
            : base(webElement, logger!, nameof(SelectWebElement))
        {
        }

        /// <summary>
        /// Selects an option by the text displayed.
        /// </summary>
        /// <param name="text"></param>
        public void SelectByText(string text)
        {
            var selectElement = new SelectElement(Element);
            selectElement.SelectByText(text);
        }

        /// <summary>
        /// Selects an option by the value.
        /// </summary>
        /// <param name="value"></param>
        public void SelectByValue(string value)
        {
            var selectElement = new SelectElement(Element);
            selectElement.SelectByValue(value);
        }

        /// <summary>
        /// Selects an option by the index.
        /// </summary>
        /// <param name="index"></param>
        public void SelectByIndex(int index)
        {
            var selectElement = new SelectElement(Element);
            selectElement.SelectByIndex(index);
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        /// <returns></returns>
        public string GetSelectedText()
        {
            var selectElement = new SelectElement(Element);
            return selectElement.SelectedOption.Text;
        }

        /// <summary>
        /// Gets the selected value.
        /// </summary>
        /// <returns></returns>
        public string GetSelectedValue()
        {
            var selectElement = new SelectElement(Element);
            return selectElement.SelectedOption.GetAttribute("value");
        }

        /// <summary>
        /// Indicates if the select element supports multiple selections.
        /// </summary>
        /// <returns></returns>
        public bool IsMultiple()
        {
            var selectElement = new SelectElement(Element);
            return selectElement.IsMultiple;
        }

        /// <summary>
        /// Gets the options text.
        /// </summary>
        /// <returns></returns>
        public List<string> GetOptionsValue()
        {
            var selectElement = new SelectElement(Element);
            return selectElement.Options.Select(o => o.GetAttribute("value")).ToList();
        }
    }
}
