using System.Data.Common;

namespace Dneprokos.SqlDb.Base.Client.SqlCore.ConcreateImplementations
{
    public abstract class BaseSqlConnectionManager
    {
        /// <summary>
        /// Database connection property that will be used by child classes for creating of specific connection
        /// This property will be used for executing of SQL queries
        /// </summary>
        public DbConnection? DbConnection { get; set; }
    }
}
