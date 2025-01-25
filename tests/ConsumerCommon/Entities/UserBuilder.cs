using Bogus;
using CommonTests.Cryptography;
using Consumer.Domain.Entities;

namespace ConsumerCommon.Entities;
public class UserBuilder
{
    public static User Build()
    {
        var passwordEncripter = new PasswordEncripterBuilder().Build();

        var user = new Faker<User>()
           .RuleFor(u => u.Id, _ => 1)
           .RuleFor(u => u.UserId, _ => Guid.NewGuid())
           .RuleFor(u => u.Username, faker => faker.Person.UserName)
           .RuleFor(u => u.Password, (_, user) => passwordEncripter.Encrypt(user.Password));

        return user;
    }
}
