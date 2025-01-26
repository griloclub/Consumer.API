using Bogus;
using Consumer.Domain.Entities;

namespace ConsumerCommon.Entities;
public class TableDetailsBuilder
{
    public static Table Build()
    {
        var faker = new Faker();
        return new Faker<Table>()
            .RuleFor(t => t.Id, 1)
            .RuleFor(t => t.Number, f => f.Random.Int(1, 10))
            .RuleFor(t => t.ClientName, f => f.Name.FullName())
            .RuleFor(t => t.Quantity, f => f.Random.Int(1, 5))
            .RuleFor(t => t.Items, _ => new List<Item>
            {
               new Item {
                   Name = faker.Commerce.ProductName(),
                   TableId = 1,
                   Price = 10.0m,
                   Quantity = 2,
                   Additions = new List<Addition> {new() {Name = "Batata Frita",Price = 2.0m}}
               },
               new Item {
                   Name = faker.Commerce.ProductName(),
                   TableId = 1,
                   Price = 15.0m,
                   Quantity = 1,
                   Options = new List<Option> {new() {Name = "Frango"}}
               }
            });
    }

    public static Table BuildInvalid() => new()
    {
        Id = 1,
        Number = 0,
        ClientName = string.Empty,
        Items = new List<Item>()
    };

    public static Table BuildNoItems()
    {
        return new Faker<Table>()
            .RuleFor(t => t.Id, _ => 1)
            .RuleFor(t => t.Number, f => f.Random.Int(1, 10))
            .RuleFor(t => t.ClientName, f => f.Name.FullName())
            .RuleFor(t => t.Quantity, f => f.Random.Int(1, 5))
            .RuleFor(t => t.Items, _ => new List<Item>());
    }
}