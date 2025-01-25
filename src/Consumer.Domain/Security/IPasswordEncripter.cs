namespace Consumer.Domain.Security;
public interface IPasswordEncripter
{
    string Encrypt(string password);
    bool VerificationPassword(string password, string passwordHash);
}