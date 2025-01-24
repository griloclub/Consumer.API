using Consumer.Domain.Enums;

namespace Consumer.Domain.Entities;

public class Table
{
    public long Id { get; set; }
    public int Number { get; set; }
    // Quem abriu a mesa
    public long UserId { get; set; }
    public TableStatus Status { get; set; }
    public DateTime OpenedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public List<OrderDetail> Items { get; set; } = [];
}