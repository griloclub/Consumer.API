using Consumer.Application.Mapper;
using Consumer.Application.UseCases.User;
using Consumer.Application.UseCases.User.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Application;
public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMaper(services);
        AddUseCases(services);
    }

    public static void AddAutoMaper(IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddAutoMapper(typeof(AutoMapping));
    }

    public static void AddUseCases(IServiceCollection serviceDescriptors)
    {
        //User
        serviceDescriptors.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
    }
}