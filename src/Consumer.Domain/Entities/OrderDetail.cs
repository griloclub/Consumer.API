namespace Consumer.Domain.Entities;

public class OrderDetail
{
    public long Id { get; set; }
    // Referência direta à mesa
    public long TableId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime TotalTime { get; set; }
    public List<Item> Itens { get; set; } = [];
    public decimal Total { get; set; }
    public decimal ServiceFee { get; set; }
    public decimal TotalPayment { get; set; }
}

public class Item
{
    public long Id { get; set; }
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
}

public class Option
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}