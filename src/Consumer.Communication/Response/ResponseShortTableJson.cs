namespace Consumer.Communication.Response;
public class ResponseShortTableJson
{
    public int Quantity { get; set; }
    public List<ResponseInformationTableJson> Tables { get; set; } = [];
}
