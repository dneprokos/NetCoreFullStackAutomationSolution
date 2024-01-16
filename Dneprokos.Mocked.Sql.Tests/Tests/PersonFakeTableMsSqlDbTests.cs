using Dneprokos.SqlDb.Base.Client.SqlCore;
using NUnit.Framework;

namespace Dneprokos.Mocked.Sql.Tests.Tests
{
    [TestFixture]
    public class PersonFakeTableMsSqlDbTests
    {
        [Test]
        public void CreateAndPopulatePersonTableTest()
        {
            //Arrange
            var createCommand = @"
                CREATE TABLE Person
                (
                    PersonId INT IDENTITY(1,1) PRIMARY KEY,
                    FirstName NVARCHAR(100),
                    LastName NVARCHAR(100),
                    IsActive BIT
                );";

            //Act
            new CoreSqlExecutionManager(
                SupportedSqlProviders.MsSql, GlobalFixtureSetup.SqlConnectionString!)
                .ExecuteQuerySingle<string>(createCommand);

            //Assert
        }
    }
}
