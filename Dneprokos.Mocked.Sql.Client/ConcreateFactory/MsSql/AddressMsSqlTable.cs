using Dneprokos.Mocked.Sql.Client.AbstractFactory.TableAbstraction;
using Dneprokos.SqlDb.Base.Client.SqlCore;
using Microsoft.Extensions.Logging;

namespace Dneprokos.Mocked.Sql.Client.ConcreateFactory.MsSql
{
    public class AddressMsSqlTable : CoreSqlExecutionManager, IAddressSqlTable
    {
        public AddressMsSqlTable(
            SupportedSqlProviders provider, string connectionString, 
            int maxRowsToDisplay, ILogger? logger) 
            : base(provider, connectionString, logger!, maxRowsToDisplay) 
        {
            
        }

        public IAddressSqlTable GetAddressById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
