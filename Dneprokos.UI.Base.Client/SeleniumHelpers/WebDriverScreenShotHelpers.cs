using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Dneprokos.UI.Base.Client.SeleniumHelpers
{
    /// <summary>
    /// Helpers to make screenshots
    /// </summary>
    public static class WebDriverScreenShotHelpers
    {
        /// <summary>
        /// Makes a screenshot and save it to the TestOutput/Screenshots folder
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="fileBaseName">File name. Should include file format extension. E.g. '.png'</param>
        /// <returns></returns>
        public static string MakeScreenShot(this IWebDriver driver, string fileBaseName)
        {
            fileBaseName.Should().NotBeNullOrEmpty("File name should be specified");

            Screenshot screenshots = ((ITakesScreenshot)driver).GetScreenshot();
            string screenshotsPath = CreateScreenShotDirectory(fileBaseName);
            string fullName = $"{fileBaseName}-{DateTime.Now:ddMMHm}.png";

            string fullFilePath = Path.Combine(screenshotsPath, fullName);

            screenshots!.SaveAsFile(fullFilePath);

            return fullFilePath;
        }

        /// <summary>
        /// Make a screenshot and save it to the TestOutput/Screenshots folder
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="fileBaseName">File name. Should include file format extension. E.g. '.png'</param>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static string MakeScreenShot(this IWebDriver driver, string fileBaseName, string directory)
        {
            fileBaseName.Should().NotBeNullOrEmpty("File name should be specified");
            directory.Should().NotBeNullOrEmpty("Directory should be specified");

            Screenshot screenshots = ((ITakesScreenshot)driver).GetScreenshot();
            string screenshotsPath = CreateScreenShotDirectory(fileBaseName, directory);
            string fullName = $"{fileBaseName}-{DateTime.Now:ddMMHm}.png";

            string fullFilePath = Path.Combine(screenshotsPath, fullName);
            screenshots!.SaveAsFile(fullFilePath);

            return fullFilePath;
        }

        /// <summary>
        /// Makes a screenshot and save it to the TestOutput/Screenshots folder. Works only if test failed
        /// </summary>
        /// <param name="driver">Current WebDriver instance</param>
        /// <param name="fileBaseName">File name. Should include file format extension. E.g. '.png'</param>
        /// <param name="shouldBeAttached">Do you want to attach screen-shot to failed test?</param>
        public static void MakeScreenShotOnFailure(
            this IWebDriver driver, string fileBaseName, bool shouldBeAttached = false)
        {
            fileBaseName.Should().NotBeNullOrEmpty("File name should be specified");

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                string filePath = driver.MakeScreenShot(fileBaseName);

                if (shouldBeAttached)
                    TestContext.AddTestAttachment(filePath);
            }
        }

        /// <summary>
        /// Makes a screenshot and save it to the TestOutput/Screenshots folder. Works only if test failed
        /// </summary>
        /// <param name="driver">Current WebDriver instance</param>
        /// <param name="fileBaseName">File name. Should include file format extension. E.g. '.png'</param>
        /// <param name="directory">Screenshots directory</param>
        /// <param name="shouldBeAttached">Do you want to attach screen-shot to failed test?</param>
        public static void MakeScreenShotOnFailure(
            this IWebDriver driver, string fileBaseName, string directory, bool shouldBeAttached = false)
        {
            fileBaseName.Should().NotBeNullOrEmpty("File name should be specified");
            directory.Should().NotBeNullOrEmpty("Directory should be specified");

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                string filePath = driver.MakeScreenShot(fileBaseName, directory);

                if (shouldBeAttached)
                    TestContext.AddTestAttachment(filePath);
            }
        }

        #region Private helpers

        private static string CreateScreenShotDirectory(string folderName, string directory)
        {
            string path = Path.Combine(directory!, "TestOutput/Screenshots", folderName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        private static string CreateScreenShotDirectory(string folderName)
        {
            string directory = Path.GetDirectoryName(typeof(Screenshot)!.Assembly!.Location!)!;
            return CreateScreenShotDirectory(folderName, directory);
        }

        #endregion
    }
}
