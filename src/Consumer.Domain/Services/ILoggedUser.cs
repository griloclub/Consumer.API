using Consumer.Domain.Entities;

namespace Consumer.Domain.Services;
public interface ILoggedUser
{
    Task<User> GetUser();
}