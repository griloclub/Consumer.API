namespace Consumer.Exception.ExceptionBase;
public abstract class ConsumerException : SystemException
{
    protected ConsumerException(string message) : base(message) { }
    public abstract int StatusCode { get; }
    public abstract List<Object> GetErros();
}