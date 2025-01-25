using Bogus;
using Consumer.Communication.Request;

namespace CommonTests.Requests;
public class RequestLoginUserJsonBuilder
{
    public static RequestLoginUserJson Build()
    {
        return new Faker<RequestLoginUserJson>()
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "#Aa1"));
    }
}