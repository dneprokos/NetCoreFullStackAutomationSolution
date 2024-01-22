using Dneprokos.HerokuApp.UI.Client.Pages.DataTablesComponents;
using Dneprokos.UI.Base.Client.TestBaseClasses;
using Dneprokos.UI.Base.Client.WebDriverCore;

namespace Dneprokos.HerokuApp.UI.Client.Pages
{
    public class DataTablesPage : FrameworkBasePage
    {
        public static DataTablesPage Instance => new();

        public ExampleOneTableComponent ExampleOneTable => new();

        #region Actions

        public DataTablesPage NavigateToDataTablesPage(string baseUrl)
        {
            ConcurrentDriverManager
                .CurrentDriver.Navigate().GoToUrl(baseUrl + "/tables");
            WaitForPageToLoad();
            return this;
        }

        

        #endregion
    }
}
