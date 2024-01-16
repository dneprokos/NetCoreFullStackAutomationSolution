using Dneprokos.UI.Base.Client.Loggers;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using Dneprokos.UI.Base.Client.WebDriverCore;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Dneprokos.UI.Base.Client.ComponentWrappers
{
    /// <summary>
    /// Basic WebElement class that all other WebElement classes should inherit from.
    /// </summary>
    public class BaseWebElement
    {
        public IWebElement Element { get; private set; }
        protected readonly ILogger? Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        public BaseWebElement(IWebElement webElement)
        {
            Logger = InternalLogger.Logger;
            Element = webElement;
            Logger?.LogInformation($"Creating an instance of {nameof(BaseWebElement)}");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        public BaseWebElement(IWebElement webElement, ILogger logger)
        {
            Element = webElement;
            Logger = logger;
            Logger?.LogInformation($"Creating an instance of {nameof(BaseWebElement)}");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="concreteElement">Element name for logger</param>
        public BaseWebElement(IWebElement webElement, string concreteElement)
        {
            Logger = InternalLogger.Logger;
            Element = webElement;
            Logger?.LogInformation($"Creating an instance of {concreteElement}");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWebElement"/> class.
        /// </summary>
        /// <param name="webElement">IWebElement</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="concreteElement">Element name for logger</param>
        public BaseWebElement(IWebElement webElement, ILogger logger, string concreteElement)
        {
            Element = webElement;
            Logger = logger;
            Logger?.LogInformation($"Creating an instance of {concreteElement}");
        }

        /// <summary>
        /// Is the element displayed on the page?
        /// </summary>
        /// <returns></returns>
        public bool IsDisplayed()
        {
            Logger?.LogInformation($"Checking if element is displayed: {Element}");
            return Element.Displayed;
        }

        /// <summary>
        /// Gets attribute value of the element
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttributeValue(string attributeName)
        {
            Logger?.LogInformation($"Getting attribute value: {attributeName}");
            return Element.GetAttribute(attributeName);
        }

        /// <summary>
        /// Is the attribute present on the element?
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public bool isAttributePresent(string attributeName)
        {
            try
            {
                Logger?.LogInformation($"Checking if attribute is present: {attributeName}");
                return !string.IsNullOrEmpty(GetAttributeValue(attributeName));
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets CSS value of the element
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetCssValue(string propertyName)
        {
            Logger?.LogInformation($"Getting CSS value: {propertyName}");
            return Element.GetCssValue(propertyName);
        }

        /// <summary>
        /// Scrolls to the element
        /// </summary>
        public void ScrollToElement()
        {
            Logger?.LogInformation($"Scrolling to element: {Element}");
            JavaScriptHelpers.RunJavaScript("arguments[0].scrollIntoView(true);", Element);
        }

        /// <summary>
        /// Scrolls to the element
        /// </summary>
        /// <param name="webDriver"></param>
        public void ScrollToElement(IWebDriver webDriver)
        {
            Logger?.LogInformation($"Scrolling to element: {Element}");
            JavaScriptHelpers.RunJavaScript(webDriver, "arguments[0].scrollIntoView(true);", Element);
        }

        /// <summary>
        /// Scrolls to the element
        /// </summary>
        /// <param name="webElement">Element scroll to</param>
        public void ScrollToElement(BaseWebElement webElement)
        {
            Logger?.LogInformation($"Scrolling to element: {Element}");
            JavaScriptHelpers.RunJavaScript("arguments[0].scrollIntoView(true);", webElement.Element);
        }

        /// <summary>
        /// Moves to the element
        /// </summary>
        public void MoveToElement()
        {
            Logger?.LogInformation($"Moving to element: {Element}");
            new Actions(ConcurrentDriverManager.CurrentDriver)
                .MoveToElement(Element)
                .Perform();
        }

        /// <summary>
        /// Moves to the element
        /// </summary>
        /// <param name="webDriver">WebDriver</param>
        public void MoveToElement(IWebDriver webDriver)
        {
            Logger?.LogInformation($"Moving to element: {Element}");
            new Actions(webDriver)
                .MoveToElement(Element)
                .Perform();
        }

        /// <summary>
        /// Moves to the element
        /// </summary>
        /// <param name="webElement"></param>
        public void MoveToElement(BaseWebElement webElement)
        {
            Logger?.LogInformation($"Moving to element: {webElement.Element}");
            new Actions(ConcurrentDriverManager.CurrentDriver)
                .MoveToElement(webElement.Element)
                .Perform();
        }

        /// <summary>
        /// Drags and drops the element
        /// </summary>
        /// <param name="targetElement">Drop element location</param>
        public void DragAndDrop(IWebElement targetElement)
        {
            Logger?.LogInformation($"Dragging and dropping element: {Element}");
            new Actions(ConcurrentDriverManager.CurrentDriver)
                .DragAndDrop(Element, targetElement)
                .Perform();
        }

        /// <summary>
        /// Drags and drops the element
        /// </summary>
        /// <param name="targetElement">Target drop position</param>
        public void DragAndDrop(BaseWebElement targetElement)
        {
            Logger?.LogInformation($"Dragging and dropping element: {Element}");
            new Actions(ConcurrentDriverManager.CurrentDriver)
                .DragAndDrop(Element, targetElement.Element)
                .Perform();
        }

        /// <summary>
        /// Drags and drops the element
        /// </summary>
        /// <param name="targetElement">Drop element location</param>
        public void JavaScriptDragAndDrop(IWebElement targetElement)
        {
            Logger?.LogInformation($"Dragging and dropping element: {Element}");
            string script = @"
                                var src=arguments[0],
                                    tgt=arguments[1];
                                var dataTransfer = new DataTransfer(); 
                                src.dispatchEvent(new DragEvent('dragstart', {dataTransfer: dataTransfer}));
                                tgt.dispatchEvent(new DragEvent('drop', {dataTransfer: dataTransfer}));
                                src.dispatchEvent(new DragEvent('dragend', {dataTransfer: dataTransfer}));
                            ";

            JavaScriptHelpers.RunJavaScript(script, Element, targetElement);
        }

        /// <summary>
        /// Drags and drops the element
        /// </summary>
        /// <param name="targetElement">Target drop position</param>
        public void JavaScriptDragAndDrop(BaseWebElement targetElement)
        {
            Logger?.LogInformation($"Dragging and dropping element: {Element}");
            string script = @"
                                var src=arguments[0],
                                    tgt=arguments[1];
                                var dataTransfer = new DataTransfer(); 
                                src.dispatchEvent(new DragEvent('dragstart', {dataTransfer: dataTransfer}));
                                tgt.dispatchEvent(new DragEvent('drop', {dataTransfer: dataTransfer}));
                                src.dispatchEvent(new DragEvent('dragend', {dataTransfer: dataTransfer}));
                            ";

            JavaScriptHelpers.RunJavaScript(script, Element, targetElement.Element);
        }
    }
}
