using Consumer.Domain.Entities;
using Consumer.Domain.Security;
using Moq;

namespace CommonTests.Token;
public class JwtTokenGeneratorBuilder
{
    private readonly Mock<IAccessTokenGenerator> _tokenGenerator;
    private string? _password;
    private User? _user;

    public JwtTokenGeneratorBuilder()
    {
        _tokenGenerator = new Mock<IAccessTokenGenerator>();
    }

    public JwtTokenGeneratorBuilder WithPassword(string? password)
    {
        _password = password;
        return this;
    }

    public JwtTokenGeneratorBuilder WithUser(User user)
    {
        _user = user;
        _tokenGenerator.Setup(t => t.GenerateAccessToken(_user))
                      .Returns("token_test");
        return this;
    }

    public IAccessTokenGenerator Build() => _tokenGenerator.Object;
}