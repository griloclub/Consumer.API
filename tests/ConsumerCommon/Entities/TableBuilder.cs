using Bogus;
using Consumer.Domain.Entities;
using Consumer.Domain.Enums;

namespace ConsumerCommon.Entities;
public class TableBuilder
{
    private readonly Faker<Table> _faker;
    private int _count = 0;

    public TableBuilder()
    {
        _faker = new Faker<Table>()
            .RuleFor(t => t.Id, f => f.Random.Long(1, 100))
            .RuleFor(t => t.Number, f => f.Random.Int(1, 10))
            .RuleFor(t => t.ClientName, f => f.Name.FullName())
            .RuleFor(t => t.Quantity, f => f.Random.Int(1, 5))
            .RuleFor(t => t.OpenedAt, f => f.Date.Past())
            .RuleFor(t => t.UserId, _ => 1)
            .RuleFor(t => t.Status, f => f.PickRandom<TableStatus>())
            .RuleFor(t => t.ClosedAt, f =>
            {
                _count++;
                return _count % 2 == 0 ? f.Date.Past() : null;
            })
            .RuleFor(t => t.Items, f => new List<Item>())
            .RuleFor(t => t.User, f => null!);
    }

    public List<Table> Collection(User user, uint count = 2)
    {
        return count == 0 ? new List<Table>() : _faker.Generate(Convert.ToInt32(count));
    }

    public Table Build(User user) => _faker.Generate();
}