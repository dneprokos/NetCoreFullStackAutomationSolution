using Dneprokos.HerokuApp.UI.Client.Pages;
using Dneprokos.HerokuApp.UI.Tests.BaseClasses;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace Dneprokos.HerokuApp.UI.Tests.E2E_Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class CheckBoxesPageTests : HerokuAppBaseTests
    {
        [Test]
        public void CheckAndUncheckCheckboxes_ShouldWork()
        {
            // Act
            var checkBoxesPage = CheckBoxesPage.Instance
                .NavigateToCheckBoxesPage(BaseUrl!)
                .CheckCheckBox1()
                .UnCheckCheckBox2();

            // Assert
            using (new AssertionScope())
            {
                checkBoxesPage.CheckBox1CheckBox().IsChecked().Should().BeTrue();
                checkBoxesPage.CheckBox2CheckBox().IsChecked().Should().BeFalse();
            }   
        }
    }
}
