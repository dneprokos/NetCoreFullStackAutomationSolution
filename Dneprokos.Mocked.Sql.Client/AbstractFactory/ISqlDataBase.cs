using Dneprokos.Mocked.Sql.Client.AbstractFactory.TableAbstraction;
using Dneprokos.Mocked.Sql.Client.Models;

namespace Dneprokos.Mocked.Sql.Client.AbstractFactory
{
    public interface ISqlDataBase
    {
        IPersonSqlTable PersonTable();

        IAddressSqlTable AddressTable();
    }
}
