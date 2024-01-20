using Dneprokos.HerokuApp.UI.Client.PageModels.DataTablesPage;
using Dneprokos.HerokuApp.UI.Client.Pages;
using Dneprokos.HerokuApp.UI.Client.Pages.DataTablesComponents;
using Dneprokos.HerokuApp.UI.Tests.BaseClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Dneprokos.HerokuApp.UI.Tests.E2E_Tests
{
    [Parallelizable(ParallelScope.All)]
    public class SortableDataTablesPageUiTests : HerokuAppBaseTests
    {
        [Test]
        [NonParallelizable]
        public void ExampleOneTable_GetTextContent_VerifyTextContentUiTest()
        {
            //Arrange
            List<ExampleOneTableModel> expectedContent = new List<ExampleOneTableModel>() 
            { 
                new ExampleOneTableModel { LastName = "Conway", FirstName = "Tim", Email = "tconway@earthlink.net", Due = "$50.00", WebSite = "http://www.timconway.com" },
                new ExampleOneTableModel { LastName = "Smith", FirstName = "John", Email = "jsmith@gmail.com", Due = "$50.00", WebSite = "http://www.jsmith.com" },
                new ExampleOneTableModel { LastName = "Doe", FirstName = "Jason", Email = "jdoe@hotmail.com", Due = "$100.00", WebSite = "http://www.jdoe.com" },
                new ExampleOneTableModel { LastName = "Bach", FirstName = "Frank", Email = "fbach@yahoo.com", Due = "$51.00", WebSite = "http://www.frank.com" }
                
            };

            //Act
            List<ExampleOneTableModel> exampleOneTable = DataTablesPage.Instance
                .NavigateToDataTablesPage(BaseUrl!)
                .ExampleOneTable
                .GetTableData();

            //Assert
            exampleOneTable.Should().BeEquivalentTo(expectedContent, p => p.Excluding(p => p.Action));
        }

        [Test]
        [NonParallelizable]
        public void ExampleOneTable_SortByFirstName_VerifyTableSortedUiTest()
        {
            //Act
            List<ExampleOneTableModel> sortedTable = DataTablesPage.Instance
                .NavigateToDataTablesPage(BaseUrl!)
                .ExampleOneTable
                .SortTableByField(TableHeaderNames.FirstName)
                .GetTableData();

            //Assert
            sortedTable
                .Select(table => table.FirstName)
                .Should()
                .BeInAscendingOrder();
        }
    }
}
