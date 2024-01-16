using Dneprokos.Mocked.Sql.Client.Models;
using Dneprokos.Mocked.Sql.Client.SqlScripts;
using Dneprokos.Mocked.Sql.Tests.BaseClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Dneprokos.Mocked.Sql.Tests.Tests
{
    [TestFixture]
    public class PersonFakeTableMsSqlDbTests : DbTestsBase
    {
        [Test]
        public void CreateAndPopulatePersonTableTest()
        {
            //Arrange
            SqlScriptsRunner!
                .ExecuteQuerySingleOrDefault<object>(PersonTableSqlScripts.CreateTableScript);
            SqlScriptsRunner!
                .ExecuteQuerySingleOrDefault<object>(PersonTableSqlScripts.InsertDefaultValuesScript);
            var expectedPersons = new List<PersonDbModel>
            {
                new PersonDbModel
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    IsActive = true
                },
                new PersonDbModel
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    IsActive = false
                },
            };

            //Act
            List<PersonDbModel> allPersons = SqlScriptsRunner
                .ExecuteQuery<PersonDbModel>("SELECT * FROM PERSON")
                .ToList();

            //Assert
            allPersons.Should().HaveCount(expectedPersons.Count);
            allPersons.Should()
                .BeEquivalentTo(expectedPersons, p => p.Excluding(p => p.Id));
        }
    }
}
