using Dneprokos.Helper.Base.Client.Loggers.Managers;
using Dneprokos.Mocked.Sql.Client.AbstractFactory;
using Dneprokos.Mocked.Sql.Client.ConcreateFactory;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Testcontainers.MsSql;

namespace Dneprokos.Mocked.Sql.Tests
{
    [SetUpFixture]
    public class GlobalFixtureSetup
    {
        public static MsSqlContainer? MsSqlContainer;
        
        public static ILogger? Logger { get; private set; }

        public static string? SqlConnectionString { get; private set; }

        public static ISqlDataBase? SqlDataBase { get; private set; }

        [OneTimeSetUp]
        public async Task BeforeAllTestFixturesAsync()
        {
            Logger = NLogLogger.Instance!.Logger;
            await StartSqlServerContainer();

            SqlDataBase = new MsSqlDataBase(SqlConnectionString!, 10, Logger);
        }

        [OneTimeTearDown]
        public async Task AfterAllTestFixtures()
        {
            await StopSqlServerContainer();
        }

        private static async Task StartSqlServerContainer()
        {
            // Build SQL Server container
            MsSqlContainer = new MsSqlBuilder().Build();
            await MsSqlContainer.StartAsync();

            SqlConnectionString = MsSqlContainer.GetConnectionString();
        }

        private static async Task StopSqlServerContainer()
        {
            await MsSqlContainer!.DisposeAsync().AsTask();
        }
    }
}
