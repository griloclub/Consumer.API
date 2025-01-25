using Consumer.Domain.Entities;
using Consumer.Domain.Repositories.Tables;
using Consumer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Infrastructure.Repositories;
internal class TableRepository : ITablesReadOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TableRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Table>> GetAllTables()
    {
        return await _dbContext.Tables.AsNoTracking().ToListAsync();
    }

    public async Task<List<Table>> GetOpenTables(User user)
    {
        return await _dbContext.Tables.AsNoTracking().Where(x => x.UserId.Equals(user.Id)).ToListAsync();
    }
}