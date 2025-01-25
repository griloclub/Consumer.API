using Consumer.Domain.Entities;
using Consumer.Domain.Services;
using Moq;

namespace CommonTests.LoggedUser;
public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();
        mock.Setup(user => user.GetUser()).ReturnsAsync(user);

        return mock.Object;
    }
}