using Dneprokos.HerokuApp.UI.Client.Pages;
using Dneprokos.HerokuApp.UI.Tests.BaseClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Dneprokos.HerokuApp.UI.Tests.E2E_Tests
{
    [Parallelizable(ParallelScope.All)]
    public class DragAndDropPageUiTests : HerokuAppBaseTests
    {
        [Test]
        public void DragAndDrop_ComponentAtoB_ComponentsExchangeUiTest()
        {
            //Arrange
            DragAndDropPage page = DragAndDropPage.Instance.NavigateToDragAndDropPage(BaseUrl!);
            string expectedText = page.GetSquareAText();

            //Act
            string squareBText = page
                .DragAndDropSquareAtoSquareB()
                .GetSquareBText();

            //Assert
            squareBText.Should().Be(expectedText);
        }
    }
}
