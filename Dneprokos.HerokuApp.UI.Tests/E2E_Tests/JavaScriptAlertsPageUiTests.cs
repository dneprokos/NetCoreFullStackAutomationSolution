using Dneprokos.Helper.Base.Client.RandomGenerators;
using Dneprokos.HerokuApp.UI.Client.Pages;
using Dneprokos.HerokuApp.UI.Tests.BaseClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Dneprokos.HerokuApp.UI.Tests.E2E_Tests
{
    [Parallelizable(ParallelScope.All)]
    public class JavaScriptAlertsPageUiTests : HerokuAppBaseTests
    {
        [Test]
        public void JsAlers_ClickForJsPromptAndEnterText_TextIsAddedUiTest()
        {
            //Arrange
            string randomText = StringGenerator.GenerateRandomString(10);
            var expectedText = $"You entered: {randomText}";

            //Act
            string textResults = JavaScriptAlertsPage.Instance
                .NavigateToAlertsPage(BaseUrl!)
                .ClickForJSPrompt()
                .SendTextToAlertAndAccept(randomText)
                .GetResultText();

            //Assert
            textResults.Should().Be(expectedText);
        }
    }
}
