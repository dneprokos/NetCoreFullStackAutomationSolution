using Dneprokos.SqlDb.Base.Client.SqlCore.ConcreateImplementations;
using System.Data.Common;

namespace Dneprokos.SqlDb.Base.Client.SqlCore.Factory
{
    public class SqlConnectionFactory
    {
        /// <summary>
        /// DbConnection to the database. Will be used for query execution.
        /// </summary>
        public DbConnection? DbConnection { get; private set; }

        /// <summary>
        /// Constructor. Creates a new instance of the SqlConnectionFactory.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connectionString"></param>
        /// <param name="logger"></param>
        /// <exception cref="NotImplementedException"></exception>
        public SqlConnectionFactory(SupportedSqlProviders provider, string connectionString)
        {
            DbConnection = provider switch
            {
                SupportedSqlProviders.MsSql => new MsSqlConnectionManager(connectionString).DbConnection,
                SupportedSqlProviders.PostgreSql => new PosgreSqlConnectionManager(connectionString).DbConnection,
                SupportedSqlProviders.OracleSql => new OracleSqlConnectionManager(connectionString).DbConnection,
                _ => throw new NotImplementedException($"The provider {provider} is not implemented."),
            };
        }
    }
}
