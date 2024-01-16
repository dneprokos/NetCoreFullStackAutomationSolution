using Dneprokos.UI.Base.Client.Loggers;
using Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System.Collections.Concurrent;
using System.Reflection;

namespace Dneprokos.UI.Base.Client.WebDriverCore
{
    public class ConcurrentDriverManager
    {
        /// <summary>
        /// Collection of web drivers and their corresponding test ids.
        /// </summary>
        private static readonly ConcurrentDictionary<IWebDriver, string> WebDriversCollection
            = new();

        /// <summary>
        /// Locker object for the collection.
        /// </summary>
        private static readonly object CollectionLocker = new();

        private static readonly ILogger Log = InternalLogger.Logger;

        /// <summary>
        /// Gets or sets the driver context to be used in the current test.
        /// </summary>
        public static IWebDriver CurrentDriver
        {
            get
            {
                return WebDriversCollection.First(collection 
                    => collection.Value == TestContext.CurrentContext.Test.ID).Key;
            }
            set => WebDriversCollection.TryAdd(value, TestContext.CurrentContext.Test.ID);
        }

        /// <summary>
        /// Starts a new driver for the current test.
        /// </summary>
        /// <param name="driverOptions"></param>
        /// <exception cref="ArgumentException"></exception>
        public void StartTestDriver(BrowserOptions driverOptions)
        {
            if (driverOptions.Equals(null))
            {
                throw new ArgumentException(MethodBase.GetCurrentMethod()!.Name);
            }

            TestContext.TestAdapter testContext = TestContext.CurrentContext.Test;

            //Lock collection and check if there is a driver with empty value
            //Then update the value with the current test id and return
            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(string.Empty)))
                {
                    IWebDriver driver = WebDriversCollection.First(dict => dict.Value.Equals(string.Empty)).Key;

                    Log?.LogInformation($"Updating driver for the test: {testContext.Name}");
                    WebDriversCollection.TryUpdate(driver, testContext.ID, string.Empty);

                    return;
                }
            }

            Log?.LogInformation($"Creating new driver for the test: {testContext.Name}");
            //If there is no driver with empty value, create a new one and add it to the collection
            CurrentDriver = DriverFactory.CreateWebDriver(driverOptions);
            WebDriversCollection.TryAdd(CurrentDriver, testContext.ID);
        }

        /// <summary>
        /// Starts a new driver for the specific id.
        /// </summary>
        /// <param name="driverOptions"></param>
        /// <param name="specificId"></param>
        /// <exception cref="ArgumentException"></exception>
        public void StartTestDriver(BrowserOptions driverOptions, string specificId)
        {
            if (driverOptions.Equals(null))
            {
                throw new ArgumentException(MethodBase.GetCurrentMethod()!.Name);
            }

            //Lock collection and check if there is a driver with empty value
            //Then update the value with the current test id and return
            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(string.Empty)))
                {
                    IWebDriver driver = WebDriversCollection.First(dict => dict.Value.Equals(string.Empty)).Key;

                    Log?.LogInformation($"Updating driver for the test: {specificId}");
                    WebDriversCollection.TryUpdate(driver, specificId, string.Empty);

                    return;
                }
            }

            Log?.LogInformation($"Creating new driver for the test: {specificId}");

            //If there is no driver with empty value, create a new one and add it to the collection
            CurrentDriver = DriverFactory.CreateWebDriver(driverOptions);
            WebDriversCollection.TryAdd(CurrentDriver, specificId);
        }

        /// <summary>
        /// Removes Test Id from current driver or quits driver in case of exception 
        /// </summary>
        /// <exception cref="WebDriverException"></exception>
        public void RemoveTestFromDriver()
        {
            //Lock collection and check if there is a driver with the current test id
            //Then update the value with empty string and return
            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(TestContext.CurrentContext.Test.ID)))
                {
                    IWebDriver driver = WebDriversCollection
                        .First(dict => dict.Value.Equals(TestContext.CurrentContext.Test.ID)).Key;

                    //Quits driver in case of exception and removes it from the collection
                    if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed)
                        && TestContext.CurrentContext.Result.Message!.Contains("WebDriverException"))
                    {
                        CurrentDriver.Quit();
                        WebDriversCollection.TryRemove(CurrentDriver, out var testID);

                        Log?.LogInformation($"WebDriver for test {testID} has been removed from the collection because of WebDriver exception");
                    }
                    else
                    {
                        WebDriversCollection.TryUpdate(driver, string.Empty, TestContext.CurrentContext.Test.ID);
                    }
                    return;
                }
            }

            //If there is no driver with the current test id, throw an exception
            throw new WebDriverException("There is no driver for the current test");
        }

        /// <summary>
        /// Stops the driver for the specific id. Does not remove the driver from the collection.
        /// </summary>
        /// <param name="specificId"></param>
        /// <exception cref="WebDriverException"></exception>
        public void StopTestDriver(string specificId)
        {
            //Lock collection and check if there is a driver with the current test id
            //Then update the value with empty string and return
            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(specificId)))
                {
                    IWebDriver driver = WebDriversCollection.First(dict => dict.Value.Equals(specificId)).Key;

                    Log?.LogInformation($"Updating driver for the test: {specificId}");
                    WebDriversCollection.TryUpdate(driver, string.Empty, specificId);

                    return;
                }
            }

            //If there is no driver with the current test id, throw an exception
            throw new WebDriverException("There is no driver for the current test");
        }

        /// <summary>
        /// Stops the driver for the current test. Does not remove the driver from the collection.
        public void StopTestDriver()
        {
            //Lock collection and check if there is a driver with the current test id
            //Then update the value with empty string and return
            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(TestContext.CurrentContext.Test.ID)))
                {
                    string testId = TestContext.CurrentContext.Test.ID;
                    IWebDriver driver = WebDriversCollection.First(dict => dict.Value.Equals(testId)).Key;
                    Log?.LogInformation($"Updating driver for the test: {testId}");
                    WebDriversCollection.TryUpdate(driver, string.Empty, testId);

                    return;
                }
            }

            //If there is no driver with the current test id, throw an exception
            throw new WebDriverException("There is no driver for the current test");
        }

        public void StopTestDriverAndRemoveFromPool()
        {
            //Lock collection and check if there is a driver with the current test id
            //Then update the value with empty string and return
            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(TestContext.CurrentContext.Test.ID)))
                {
                    IWebDriver driver = WebDriversCollection.First(dict => dict.Value.Equals(TestContext.CurrentContext.Test.ID)).Key;
                    Log?.LogInformation($"Stopping and removing driver for the test: {TestContext.CurrentContext.Test.ID}");
                    driver.Quit();
                    WebDriversCollection.TryRemove(driver, out var testID);
                }
            }
        }

        public void StopDriverAndRemoveFromPool(string specificId)
        {
            //Lock collection and check if there is a driver with the current test id
            //Then update the value with empty string and return
            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(specificId)))
                {
                    IWebDriver driver = WebDriversCollection.First(dict => dict.Value.Equals(specificId)).Key;
                    Log?.LogInformation($"Stopping driver and removing from the test: {specificId}");
                    driver.Quit();
                    WebDriversCollection.TryRemove(driver, out var testID);
                }
            }
        }

        /// <summary>
        /// Quits the driver for the current test.
        /// </summary>
        public void QuitCurrentTestDriver()
        {
            //Lock collection and check if there is a driver with the current test id
            //Then update the value with empty string and return
            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(TestContext.CurrentContext.Test.ID)))
                {
                    IWebDriver driver = WebDriversCollection.First(dict => dict.Value.Equals(TestContext.CurrentContext.Test.ID)).Key;
                    driver.Quit();
                    WebDriversCollection.TryRemove(driver, out var testID);
                }
            }
        }

        /// <summary>
        /// Quits all drivers in the collection.
        /// </summary>
        public void QuitAllDrivers()
        {
            //Lock collection and check if there is a driver with the current test id
            //Then update the value with empty string and return
            lock (CollectionLocker)
            {
                foreach (var driver in WebDriversCollection)
                {
                    driver.Key.Quit();
                }
            }
        }
    }
}
