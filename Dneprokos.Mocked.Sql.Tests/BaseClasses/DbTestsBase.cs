using Dneprokos.Helper.Base.Client.Configuration;
using Dneprokos.Mocked.Sql.Client.AbstractFactory;
using Dneprokos.SqlDb.Base.Client.SqlCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Dneprokos.Mocked.Sql.Tests.BaseClasses
{
    [TestFixture]
    public class DbTestsBase
    {
        public ILogger? Logger { get; private set; }
        public string? ConnectionString { get; private set; }

        public static CoreSqlExecutionManager? SqlScriptsRunner;

        public static ISqlDataBase? SqlDataBase { get; private set; }

        [OneTimeSetUp]
        public virtual void OnceBeforeFeature()
        {
            Logger = GlobalFixtureSetup.Logger!;
            ConnectionString = GlobalFixtureSetup.SqlConnectionString!;
            SqlScriptsRunner = new CoreSqlExecutionManager(
                SupportedSqlProviders.MsSql, ConnectionString!, Logger!);
            SqlDataBase = GlobalFixtureSetup.SqlDataBase!;
        }

        [SetUp]
        public virtual void OnceBeforeTest()
        {
            var instanceA = ConfigManager.Instance;
            var instanceB = ConfigManager.Instance;

            Console.WriteLine("");
            Console.WriteLine($"Hash code of instance A: {instanceA.GetHashCode()}");
            Console.WriteLine($"Hash code of instance B: {instanceB.GetHashCode()}");

            //string baseUrl = instanceA.Get("baseUrl");
        }
    }
}
