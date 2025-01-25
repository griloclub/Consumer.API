namespace Consumer.Domain.Security;
public interface ITokenProvider
{
    string TokenOnRequest();
}