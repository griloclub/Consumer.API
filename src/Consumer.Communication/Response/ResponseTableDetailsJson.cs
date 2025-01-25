using Consumer.Communication.Enums;

namespace Consumer.Communication.Response;
public class ResponseTableDetailsJson
{
    public long Id { get; set; }
    public int Number { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public TimeSpan Duration { get; set; }
    public TableStatus Status { get; set; }
    public DateTime? ClosedAt { get; set; }
    public List<ItemJson>? Items { get; set; }
    public ValuesJson Values { get; set; } = null!;
}
public class ValuesJson
{
    public decimal? Total { get; set; }
    public decimal? ServiceFee { get; set; }
    public decimal? TotalPayment { get; set; }
}
public class ItemJson
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Observation { get; set; }
    public List<AdditionJson>? Additions { get; set; }
    public List<OptionJson>? Options { get; set; }
}

public class AdditionJson
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class OptionJson
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

