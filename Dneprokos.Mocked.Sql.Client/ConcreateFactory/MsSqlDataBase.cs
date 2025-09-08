using Dneprokos.Mocked.Sql.Client.AbstractFactory;
using Dneprokos.Mocked.Sql.Client.AbstractFactory.TableAbstraction;
using Dneprokos.Mocked.Sql.Client.ConcreateFactory.MsSql;
using Dneprokos.SqlDb.Base.Client.SqlCore;
using Microsoft.Extensions.Logging;

namespace Dneprokos.Mocked.Sql.Client.ConcreateFactory
{
    public class MsSqlDataBase : ISqlDataBase
    {
        private readonly SupportedSqlProviders provider;
        private readonly string msSqlConnectionString;
        private readonly int loggerMaxRowsToDisplay;
        private readonly ILogger? logger;

        public MsSqlDataBase(
            string msSqlConnectionString, 
            int loggerMaxRowsToDisplay, 
            ILogger? logger = null)
        {
            provider = SupportedSqlProviders.MsSql;
            this.msSqlConnectionString = msSqlConnectionString;
            this.logger = logger;
            this.loggerMaxRowsToDisplay = loggerMaxRowsToDisplay;
        }

        public IPersonSqlTable PersonTable()
            => new PersonMsSqlTable(
                provider, msSqlConnectionString, loggerMaxRowsToDisplay, logger);

        public IAddressSqlTable AddressTable() 
            => new AddressMsSqlTable(
                provider, msSqlConnectionString, loggerMaxRowsToDisplay, logger);
    }
}
