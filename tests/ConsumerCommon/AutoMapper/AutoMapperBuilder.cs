using AutoMapper;
using Consumer.Application.Mapper;

namespace CommonTests.AutoMapper;
public class AutoMapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });

        return mapper.CreateMapper();
    }
}