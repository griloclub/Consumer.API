using Consumer.Domain.Entities;
using Consumer.Domain.Repositories.User;
using Moq;

namespace CommonTests.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;
    private User? _user;

    public UserReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUserReadOnlyRepository>();
    }

    public UserReadOnlyRepositoryBuilder WithUser(User user)
    {
        _user = user;
        _repository.Setup(r => r.GetByUsernameAsync(user.Username))
                  .ReturnsAsync(user);
        return this;
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}