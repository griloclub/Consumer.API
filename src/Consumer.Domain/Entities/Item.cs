namespace Consumer.Domain.Entities;

public class Item
{
    public long Id { get; set; }
    public long TableId { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Observation { get; set; }
    public List<Addition>? Additions { get; set; } = [];
    public List<Option>? Options { get; set; } = [];
}

public class Addition
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public long ItemId { get; set; }
}

public class Option
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long ItemId { get; set; }
}