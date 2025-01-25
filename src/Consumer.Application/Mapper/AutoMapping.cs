using AutoMapper;
using Consumer.Communication.Response;
using Consumer.Domain.Entities;

namespace Consumer.Application.Mapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<User, ResponseLoginUserJson>();
    }
}