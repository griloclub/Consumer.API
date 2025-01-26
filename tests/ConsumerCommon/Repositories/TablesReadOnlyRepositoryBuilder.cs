using Consumer.Domain.Entities;
using Consumer.Domain.Repositories.Tables;
using ConsumerCommon.Entities;
using Moq;

namespace ConsumerCommon.Repositories;
public class TablesReadOnlyRepositoryBuilder
{
    private readonly Mock<ITablesReadOnlyRepository> _repository;
    private User? _user;
    private List<Table>? _openTables;
    private List<Table>? _allTables;
    private Table? _tableDetails;

    public TablesReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<ITablesReadOnlyRepository>();
    }

    public TablesReadOnlyRepositoryBuilder WithUser(User user)
    {
        _user = user;
        return this;
    }

    public TablesReadOnlyRepositoryBuilder WithOpenTables(int count)
    {
        _openTables = count == 0 ? new List<Table>() : new TableBuilder().Collection(_user!, (uint)count);
        _repository.Setup(r => r.GetOpenTables(_user!)).ReturnsAsync(_openTables);
        return this;
    }

    public TablesReadOnlyRepositoryBuilder WithAllTables(int count)
    {
        _allTables = new TableBuilder().Collection(_user!, (uint)count);
        _repository.Setup(r => r.GetAllTables()).ReturnsAsync(_allTables);
        return this;
    }

    public TablesReadOnlyRepositoryBuilder WithTableDetails(Table? table)
    {
        _tableDetails = table;
        _repository.Setup(r => r.GetTableDetails(It.IsAny<long>()))
                  .ReturnsAsync(_tableDetails);
        return this;
    }

    public TablesReadOnlyRepositoryBuilder BuildOnInvalid(Table? table = null)
    {
        _repository.Setup(r => r.GetTableDetails(It.IsAny<long>())).ReturnsAsync(table);
        return this;
    }


    public ITablesReadOnlyRepository Build() => _repository.Object;
}