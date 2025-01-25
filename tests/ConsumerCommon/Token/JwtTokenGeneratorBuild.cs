using Consumer.Domain.Entities;
using Consumer.Domain.Security;
using Moq;

namespace CommonTests.Token;
public class JwtTokenGeneratorBuild
{
    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        mock.Setup(accessTokenGenerator => accessTokenGenerator.GenerateAccessToken(It.IsAny<User>())).Returns("Token");
        return mock.Object;
    }
}