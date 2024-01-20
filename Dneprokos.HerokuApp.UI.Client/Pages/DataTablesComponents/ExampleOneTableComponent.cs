using Dneprokos.HerokuApp.UI.Client.PageModels.DataTablesPage;
using Dneprokos.UI.Base.Client.ComponentWrappers;
using Dneprokos.UI.Base.Client.SeleniumHelpers;
using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.WebDriverCore;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Dneprokos.HerokuApp.UI.Client.Pages.DataTablesComponents
{
    public class ExampleOneTableComponent : FrameworkBasePage
    {
        #region Selectors

        public const string MainTableSelectorId = "table1";

        public const string TableRowsXPath = $"//table[@id='{MainTableSelectorId}']/tbody/tr";

        #endregion

        public List<ExampleOneTableModel> GetTableData()
        {
            Log?.LogInformation($"Getting Example 1 table content");

            List<ExampleOneTableModel> tableData = new();

            // Wait for table rows
            var tableRows = ConcurrentDriverManager
                .CurrentDriver
                .WaitForElements(By.XPath(TableRowsXPath));

            foreach (var row in tableRows)
            {
                var cells = row.FindElements(By.TagName("td"));

                var model = new ExampleOneTableModel
                {
                    LastName = cells[0].Text,
                    FirstName = cells[1].Text,
                    Email = cells[2].Text,
                    Due = cells[3].Text,
                    WebSite = cells[4].Text,
                    Action = new ActionButtons
                    {
                        Edit = new ButtonWebElement(cells[5].FindElement(By.XPath(".//a[@href='#edit']"))),
                        Delete = new ButtonWebElement(cells[5].FindElement(By.XPath(".//a[@href='#delete']")))
                    }
                };

                tableData.Add(model);
            }

            return tableData;
        }

        public ExampleOneTableComponent SortTableByField(TableHeaderNames headerName)
        {
            // Use the enum to get the string representation
            string fieldName = headerName.ToString();

            // Call the existing SortTableByField method
            return SortTableByField(fieldName);
        }

        public ExampleOneTableComponent SortTableByField(string fieldName)
        {
            Log?.LogInformation($"Sorting Example 1 table by {fieldName}");

            // Map the field name to the text that appears in the header
            var headerTextMap = new Dictionary<string, string>
            {
                { "LastName", "Last Name" },
                { "FirstName", "First Name" },
                { "Email", "Email" },
                { "Due", "Due" },
                { "WebSite", "Web Site" }
            };

            if (!headerTextMap.TryGetValue(fieldName, out var headerText))
            {
                throw new ArgumentException($"Invalid field name: {fieldName}");
            }

            // Find the header by text and click to sort
            var header = ConcurrentDriverManager
                .CurrentDriver.WaitUntilElementIsClickable(By.XPath($"//th[.//span[text()='{headerText}']]"));
            header.Click();

            // Add a wait for the sorting to complete if necessary

            return this;
        }
    }
}
