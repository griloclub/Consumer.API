using Consumer.Domain.Entities;

namespace Consumer.Domain.Security;
public interface IAccessTokenGenerator
{
    string GenerateAccessToken(User user);
}