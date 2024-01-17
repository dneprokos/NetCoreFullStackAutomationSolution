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
        private List<PersonDbModel>? allPersonsInDb;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            SqlScriptsRunner!
                .ExecuteQuerySingleOrDefault<object>(PersonTableSqlScripts.CreateTableScript);
            SqlScriptsRunner!
                .ExecuteQuerySingleOrDefault<object>(PersonTableSqlScripts.InsertDefaultValuesScript);

            allPersonsInDb = new List<PersonDbModel>
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
                new PersonDbModel
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Connar",
                    IsActive = true
                },
            };
        }

        [Test]
        public void ExecuteQuery_UnitTest()
        {
            //Arrange
            
            //Act
            List<PersonDbModel> allPersons = SqlScriptsRunner!
                .ExecuteQuery<PersonDbModel>("SELECT * FROM PERSON")
                .ToList();

            //Assert
            allPersons.Should().HaveCount(allPersonsInDb!.Count);
            allPersons.Should()
                .BeEquivalentTo(allPersonsInDb, p => p.Excluding(p => p.Id));
        }

        [Test]
        public void ExecuteQueryFirst_UnitTest()
        {
            //Arrange
            PersonDbModel expectedFirstPerson = allPersonsInDb!.First();

            //Act
            PersonDbModel firstPerson = SqlScriptsRunner!
                .ExecuteQueryFirst<PersonDbModel>("SELECT * FROM PERSON");

            //Assert
            firstPerson.Should().BeEquivalentTo(expectedFirstPerson);
        }

        [Test]
        public void ExecuteQueryFirstOrDefault_UnitTest()
        {
            //Arrange
            string searchFirstName = "Jane";
            PersonDbModel expectedFirstPerson = allPersonsInDb!.First(p => p.FirstName == searchFirstName);

            //Act
            PersonDbModel firstPerson = SqlScriptsRunner!
                .ExecuteQueryFirst<PersonDbModel>(
                    $"SELECT * FROM PERSON WHERE {nameof(PersonDbModel.FirstName)} = '{searchFirstName}'");

            //Assert
            firstPerson.Should().BeEquivalentTo(expectedFirstPerson);
        }

        [Test]
        public void ExecuteQuerySingle_UnitTest()
        {
            //Arrange
            string searchFirstName = "Jane";
            PersonDbModel expectedSinglePerson = allPersonsInDb!.First(p => p.FirstName == searchFirstName);

            //Act
            PersonDbModel singlePerson = SqlScriptsRunner!
                .ExecuteQuerySingle<PersonDbModel>(
                    $"SELECT * FROM PERSON WHERE {nameof(PersonDbModel.FirstName)} = '{searchFirstName}'");

            //Assert
            singlePerson.Should().BeEquivalentTo(expectedSinglePerson);
        }

        [Test]
        public void ExecuteQuerySingleOrDefault_UnitTest()
        {
            //Arrange
            string searchFirstName = "Jane";
            PersonDbModel expectedSingleOrDefaultPerson = allPersonsInDb!.First(p => p.FirstName == searchFirstName);

            //Act
            PersonDbModel singleOrDefaultPerson = SqlScriptsRunner!
                .ExecuteQuerySingleOrDefault<PersonDbModel>(
                    $"SELECT * FROM PERSON WHERE {nameof(PersonDbModel.FirstName)} = '{searchFirstName}'");

            //Assert
            singleOrDefaultPerson.Should().BeEquivalentTo(expectedSingleOrDefaultPerson);
        }
    }
}
