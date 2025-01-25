namespace Consumer.Communication.Response;
public class ResponseInformationTableJson
{
    public long Id { get; set; }
    public int Number { get; set; }
    public int Status { get; set; }
    public string OpenedAt { get; set; } = string.Empty;
    public long UserId { get; set; }
}