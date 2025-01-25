namespace Consumer.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<Entities.User?> GetByUsernameAsync(string username);
}