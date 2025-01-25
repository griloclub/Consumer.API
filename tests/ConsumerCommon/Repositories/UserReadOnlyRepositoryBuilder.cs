using Consumer.Domain.Entities;
using Consumer.Domain.Repositories.User;
using Moq;

namespace CommonTests.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUserReadOnlyRepository>();
    }

    public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
    {
        _repository.Setup(repository => repository.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
        return this;
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}