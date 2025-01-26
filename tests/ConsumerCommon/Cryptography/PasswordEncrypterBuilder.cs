using Consumer.Domain.Security;
using Moq;

namespace CommonTests.Cryptography;
public class PasswordEncrypterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;
    private string? _password;

    public PasswordEncrypterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();
        _mock.Setup(p => p.Encrypt(It.IsAny<string>())).Returns("MockPassword1!");
    }

    public PasswordEncrypterBuilder WithPassword(string? password)
    {
        _password = password;
        if (!string.IsNullOrWhiteSpace(_password))
        {
            _mock.Setup(p => p.VerificationPassword(_password, It.IsAny<string>()))
                 .Returns(true);
        }
        return this;
    }

    public IPasswordEncripter Build() => _mock.Object;
}