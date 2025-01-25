namespace Consumer.Communication.Response;
public class ResponseTableJson
{
    public ResponseShortTableJson MyOpenTables { get; set; } = new ResponseShortTableJson();
    public ResponseShortTableJson AllTables { get; set; } = new ResponseShortTableJson();
}