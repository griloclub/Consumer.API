namespace Consumer.Domain.Entities;
public class User
{
    public long Id { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}