using Oracle.ManagedDataAccess.Client;

namespace Dneprokos.SqlDb.Base.Client.SqlCore.ConcreateImplementations
{
    internal class OracleSqlConnectionManager : BaseSqlConnectionManager
    {
        public OracleSqlConnectionManager(string connectionString)
        {
            DbConnection = new OracleConnection(connectionString);
        }
    }
}