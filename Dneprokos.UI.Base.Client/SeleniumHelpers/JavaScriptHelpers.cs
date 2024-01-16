using Dneprokos.UI.Base.Client.WebDriverCore;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    /// <summary>
    /// Helper class for running javascript in the browser
    /// </summary>
    public static class JavaScriptHelpers
    {
        /// <summary>
        /// Runs javascript in the current browser and returns result
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object RunJavaScript(string script, params object[] parameters)
        {
            var jsExec = (IJavaScriptExecutor)ConcurrentDriverManager.CurrentDriver;
            try
            {
                return jsExec.ExecuteScript(script, parameters);
            }
            catch (Exception)
            {
                Console.WriteLine($"JavaScript {script} execution was failed");
                return false;
            }
        }

        /// <summary>
        /// Runs javascript in the current browser and returns result
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="script"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object RunJavaScript(this IWebDriver webDriver, string script, params object[] parameters)
        {
            var jsExec = (IJavaScriptExecutor)webDriver;
            try
            {
                return jsExec.ExecuteScript(script, parameters);
            }
            catch (Exception)
            {
                Console.WriteLine($"JavaScript {script} execution was failed");
                return false;
            }
        }

        /// <summary>
        /// Run Async javascript in the current browser
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parameters"></param>
        public static void RunJavaScriptAsync(string script, params object[] parameters)
        {
            var jsExec = (IJavaScriptExecutor)ConcurrentDriverManager.CurrentDriver;
            try
            {
                jsExec.ExecuteAsyncScript(script, parameters);
            }
            catch (Exception)
            {
                Console.WriteLine($"Async JavaScript {script} execution was failed");
            }
        }

        /// <summary>
        /// Run Async javascript in the current browser
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="script"></param>
        /// <param name="parameters"></param>
        public static void RunJavaScriptAsync(this IWebDriver webDriver, string script, params object[] parameters)
        {
            var jsExec = (IJavaScriptExecutor)webDriver;
            try
            {
                jsExec.ExecuteAsyncScript(script, parameters);
            }
            catch (Exception)
            {
                Console.WriteLine($"Async JavaScript {script} execution was failed");
            }
        }
    }
}
