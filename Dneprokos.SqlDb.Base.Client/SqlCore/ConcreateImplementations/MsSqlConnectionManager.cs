using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Dneprokos.SqlDb.Base.Client.SqlCore.ConcreateImplementations
{
    internal class MsSqlConnectionManager : BaseSqlConnectionManager
    {
        public MsSqlConnectionManager(string connectionString) 
        {
            DbConnection = new SqlConnection(connectionString);
        }
    }
}