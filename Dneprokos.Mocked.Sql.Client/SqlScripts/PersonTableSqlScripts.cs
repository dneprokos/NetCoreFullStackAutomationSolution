namespace Dneprokos.Mocked.Sql.Client.SqlScripts
{
    public class PersonTableSqlScripts
    {
        public const string CreateTableScript = @"
                CREATE TABLE Person
                (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    FirstName NVARCHAR(100),
                    LastName NVARCHAR(100),
                    IsActive BIT
                );";

        public const string InsertDefaultValuesScript = @"
                INSERT INTO Person (FirstName, LastName, IsActive)
                VALUES ('John', 'Doe', 1),
                ('Jane', 'Doe', 0),
                ('John', 'Connar', 1);";
    }
}
