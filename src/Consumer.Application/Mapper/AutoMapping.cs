using AutoMapper;
using Consumer.Communication.Response;
using Consumer.Domain.Entities;

namespace Consumer.Application.Mapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Table, ResponseInformationTableJson>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Number, src => src.MapFrom(x => x.Number))
            .ForMember(dest => dest.Status, src => src.MapFrom(x => x.Status))
            .ForMember(dest => dest.OpenedAt, src => src.MapFrom(x => x.OpenedAt.ToString("HH:mm:ss")))
            .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId));
    }
}