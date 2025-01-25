using Consumer.Domain.Entities;
using Consumer.Domain.Repositories.User;
using Consumer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Infrastructure.Repositories;
internal class UserRepository : IUserReadOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Username.ToLower().Equals(username.ToLower()));
    }
}