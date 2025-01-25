using System.Net;

namespace Consumer.Exception.ExceptionBase;
public class NotFoundException : ConsumerException
{
    public NotFoundException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<object> GetErros()
    {
        return [Message];
    }
}