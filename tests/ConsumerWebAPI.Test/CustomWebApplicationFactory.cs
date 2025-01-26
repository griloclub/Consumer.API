using Consumer.Domain.Entities;
using Consumer.Domain.Security;
using Consumer.Infrastructure.Data;
using ConsumerCommon.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resources;

namespace WebApi.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public TableIdentityManager Table { get; private set; } = default!;
    public TableIdentityManager TableDetails { get; private set; } = default!;
    public UserIdentityManager User { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<ApplicationDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDataBase(dbContext, passwordEncripter, accessTokenGenerator);
            });
    }

    private void StartDataBase(ApplicationDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = AddUser(dbContext, passwordEncripter, accessTokenGenerator);

        AddTables(dbContext, user);
        AddTableDetails(dbContext);

        dbContext.SaveChanges();
    }

    private void AddTables(ApplicationDbContext dbContext, User user)
    {
        var table = new TableBuilder().Build(user);
        dbContext.Tables.Add(table);

        Table = new TableIdentityManager(table);
    }

    private void AddTableDetails(ApplicationDbContext dbContext)
    {
        var tableDetails = TableDetailsBuilder.Build();
        dbContext.Tables.Add(tableDetails);

        TableDetails = new TableIdentityManager(tableDetails);
    }

    private User AddUser(ApplicationDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build();
        var password = user.Password;

        user.Password = passwordEncripter.Encrypt(user.Password);
        dbContext.Users.Add(user);

        var token = accessTokenGenerator.GenerateAccessToken(user);

        User = new UserIdentityManager(user, password, token);

        return user;
    }

    public CustomWebApplicationFactory WithEmptyTables()
    {
        var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Tables.RemoveRange(dbContext.Tables);
        dbContext.SaveChanges();
        return this;
    }
}