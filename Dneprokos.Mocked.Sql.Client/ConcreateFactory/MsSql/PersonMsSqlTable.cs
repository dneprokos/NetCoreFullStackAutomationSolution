using Dneprokos.Mocked.Sql.Client.AbstractFactory.TableAbstraction;
using Dneprokos.Mocked.Sql.Client.Models;
using Dneprokos.SqlDb.Base.Client.SqlCore;
using Microsoft.Extensions.Logging;

namespace Dneprokos.Mocked.Sql.Client.ConcreateFactory.MsSql
{
    public class PersonMsSqlTable : CoreSqlExecutionManager, IPersonSqlTable 
    {
        private const string TableName = "PERSON";

        public PersonMsSqlTable(
            SupportedSqlProviders provider, string connectionString, 
            int maxRowsToDisplay, ILogger? logger) 
            : base(provider, connectionString, logger!, maxRowsToDisplay)
        {
        }

        public PersonDbModel GetPersonById(int? id)
        {
            return ExecuteQueryFirstOrDefault<PersonDbModel>(
                $"SELECT * FROM {TableName} " +
                $"WHERE {nameof(PersonDbModel.Id)} = {id}");
        }
    }
}
