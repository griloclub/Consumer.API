using Consumer.Domain.Repositories.Tables;
using Consumer.Domain.Repositories.User;
using Consumer.Domain.Security;
using Consumer.Domain.Services;
using Consumer.Infrastructure.Data;
using Consumer.Infrastructure.Repositories;
using Consumer.Infrastructure.Security;
using Consumer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Infrastructure;
public static class DependecyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        services.AddScoped<IPasswordEncripter, Security.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddRepositories(services);
        AddToken(services, configuration);
    }

    private static void AddRepositories(IServiceCollection serviceDescriptors)
    {
        //User
        serviceDescriptors.AddScoped<IUserReadOnlyRepository, UserRepository>();

        //Tables
        serviceDescriptors.AddScoped<ITablesReadOnlyRepository, TableRepository>();
    }

    private static void AddToken(IServiceCollection serviceDescriptors, IConfiguration configuration)
    {
        var expirationMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signinKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        //JWT
        serviceDescriptors.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationMinutes, signinKey!));
    }

    private static void AddDbContext(IServiceCollection serviceDescriptors, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        serviceDescriptors.AddDbContext<ApplicationDbContext>(options => options.UseFirebird(connectionString));
    }
}