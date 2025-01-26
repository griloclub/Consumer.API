using Consumer.Domain.Entities;

namespace WebApi.Test.Resources;
public class TableIdentityManager
{
    private readonly Table _table;

    public TableIdentityManager(Table table)
    {
        _table = table;
    }

    public long GetId() => _table.Id;
}