using Dneprokos.HerokuApp.UI.Client.Pages;
using Dneprokos.HerokuApp.UI.Tests.BaseClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Dneprokos.HerokuApp.UI.Tests.E2E_Tests
{
    [Parallelizable(ParallelScope.All)]
    public class FileUploadPageUiTests : HerokuAppBaseTests
    {
        private string? fileName;
        private string? filePath;

        [OneTimeSetUp]
        public void BeforeFixture()
        {
            fileName = "testfile.txt";
            filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            // Create and write something to the file
            File.WriteAllText(filePath, "This is a test file for uploading.");
        }

        [OneTimeTearDown]
        public void AfterFixture()
        {
            // Delete the file after the test is done
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        [Test]
        public void FileUpload_UploadFile_ShouldUploadFileUiTest()
        {
            //Arrange

            //Act
            string uploadedFileName = FileUploadPage.Instance
                .NavigateToFileUploadPage(BaseUrl!)
                .UploadFile(filePath!)
                .GetUploadedFileName();

            //Assert
            uploadedFileName.Should().Be(fileName);
        }
    }
}
