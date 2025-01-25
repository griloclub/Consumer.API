using Consumer.Domain.Enums;

namespace Consumer.Domain.Entities;

public class Table
{
    public long Id { get; set; }
    public int Number { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime OpenedAt { get; set; }
    public long? UserId { get; set; }
    public TableStatus Status { get; set; }
    public DateTime? ClosedAt { get; set; }
    public List<Item> Items { get; set; } = [];
    public User User { get; set; } = null!;
}