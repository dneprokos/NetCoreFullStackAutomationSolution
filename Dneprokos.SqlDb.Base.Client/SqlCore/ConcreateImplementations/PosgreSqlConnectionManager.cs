using Npgsql;

namespace Dneprokos.SqlDb.Base.Client.SqlCore.ConcreateImplementations
{
    public class PosgreSqlConnectionManager : BaseSqlConnectionManager
    {
        public PosgreSqlConnectionManager(string connectionString)
        {
            DbConnection = new NpgsqlConnection(connectionString);
        }
    }
}