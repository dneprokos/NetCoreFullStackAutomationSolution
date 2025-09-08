using Dneprokos.Mocked.Sql.Client.Models;

namespace Dneprokos.Mocked.Sql.Client.AbstractFactory.TableAbstraction
{
    public interface IPersonSqlTable
    {
        PersonDbModel GetPersonById(int? id);
    }
}
