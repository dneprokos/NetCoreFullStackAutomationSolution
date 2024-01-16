using Dneprokos.Helper.Base.Client.Loggers.Managers;
using Dneprokos.SqlDb.Base.Client.Loggers;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using NUnit.Framework;

namespace Dneprokos.Mocked.Sql.Tests
{
    [SetUpFixture]
    public class GlobalFixtureSetup
    {
        public static IContainer? SqlServerContainer;

        public static string? SqlConnectionString { get; private set; }

        [OneTimeSetUp]
        public async Task BeforeAllTestFixturesAsync()
        {
            InternalLogger.Logger = NLogLogger.Instance!.Logger;
            await StartSqlServerContainer();
        }

        [OneTimeTearDown]
        public async Task AfterAllTestFixtures()
        {
            await StopSqlServerContainer();
        }

        private static async Task StartSqlServerContainer()
        {
            var imageName = "mcr.microsoft.com/mssql/server";
            var password = "Your_password123"; // Change to your desired password
            var dbName = "TestDB";
            int internalPort = 1433;

            // Build SQL Server container
            SqlServerContainer = new ContainerBuilder()
                .WithImage(imageName)
                .WithEnvironment("ACCEPT_EULA=Y", $"SA_PASSWORD={password}")
                .WithPortBinding(9998, internalPort) // Adjust the external port if needed
                .Build();

            // Start SQL Server container
            await SqlServerContainer.StartAsync();

            // Build the connection string
            var dataSource = $"{SqlServerContainer.IpAddress}:{internalPort}";
            SqlConnectionString = $"Server={dataSource};Database={dbName};User Id=sa;Password={password};";
        }

        private static async Task StopSqlServerContainer()
        {
            await SqlServerContainer!.DisposeAsync().AsTask();
        }

        
    }
}
