using Dneprokos.HerokuApp.UI.Client.Pages;
using Dneprokos.HerokuApp.UI.Tests.BaseClasses;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace Dneprokos.HerokuApp.UI.Tests.E2E_Tests
{
    [Parallelizable(ParallelScope.All)]
    public class FileDownloadPageUiTests : HerokuAppBaseTests
    {
        [Test]
        [NonParallelizable]
        public void FileDownLoad_FirstLinkDownload_ShouldDownloadUiTest()
        {
            //Arrange

            //Act
            FileDownloadPage page = FileDownloadPage.Instance.NavigateToFileDownloadPage(BaseUrl!);
            var fileName = page.GetFileNameOfFirstLink();
            string filePath = page.DownloadFistLink().WaitForFileDownloaded(fileName);

            //Assert
            using (new AssertionScope()) 
            {
                bool isFileExist = File.Exists(filePath); 
                isFileExist.Should().BeTrue("because the file should have been downloaded.");

                if (isFileExist)
                    File.Delete(filePath);
            }               
        }

        
    }
}
