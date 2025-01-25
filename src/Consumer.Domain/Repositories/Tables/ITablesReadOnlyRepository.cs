using Consumer.Domain.Entities;

namespace Consumer.Domain.Repositories.Tables;
public interface ITablesReadOnlyRepository
{
    Task<List<Table>> GetOpenTables(Entities.User user);
    Task<List<Table>> GetAllTables();
}