using System.Net;

namespace Consumer.Exception.ExceptionBase;
public class InvalidLoginException : ConsumerException
{
    public InvalidLoginException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<object> GetErros()
    {
        return [Message];
    }
}