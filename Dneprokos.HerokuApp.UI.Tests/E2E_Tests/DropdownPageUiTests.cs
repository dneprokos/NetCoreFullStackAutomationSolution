using Dneprokos.HerokuApp.UI.Client.Pages;
using Dneprokos.HerokuApp.UI.Tests.BaseClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Dneprokos.HerokuApp.UI.Tests.E2E_Tests
{
    [Parallelizable(ParallelScope.All)]
    public class DropdownPageUiTests : HerokuAppBaseTests
    {
        [Test]
        public void SelectDropdownOption_ShoulBeSelected()
        {
            // Act
            var dropdownPage = DropdownPage.Instance
                .NavigateToDropdownPage(BaseUrl!)
                .SelectOption1();

            // Assert
            dropdownPage.GetSelectedText().Should().Be("Option 1");
        }
    }
}
