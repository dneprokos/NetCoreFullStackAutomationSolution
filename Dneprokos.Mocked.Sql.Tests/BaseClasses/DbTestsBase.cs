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

        [OneTimeSetUp]
        public virtual void OnceBeforeFeature()
        {
            Logger = GlobalFixtureSetup.Logger!;
            ConnectionString = GlobalFixtureSetup.SqlConnectionString!;
            SqlScriptsRunner = new CoreSqlExecutionManager(
                SupportedSqlProviders.MsSql, ConnectionString!, Logger!);
        }
    }
}
