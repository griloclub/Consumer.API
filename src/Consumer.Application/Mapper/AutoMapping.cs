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

        CreateMap<Table, ResponseTableDetailsJson>()
           .ForMember(dest => dest.Duration, src => src.MapFrom(x => DateTime.UtcNow.Subtract(x.OpenedAt)))
           .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
           .ForMember(dest => dest.Values, opt => opt.MapFrom(src => new ValuesJson
           {
               Total = src.Items.Sum(i => i.Price * i.Quantity + (i.Additions != null ? i.Additions.Sum(a => a.Price) : 0)),
               ServiceFee = src.Items.Sum(i => i.Price * i.Quantity + (i.Additions != null ? i.Additions.Sum(a => a.Price) : 0)) * 0.1m,
               TotalPayment = src.Items.Sum(i => i.Price * i.Quantity + (i.Additions != null ? i.Additions.Sum(a => a.Price) : 0)) * 1.1m
           }));

        CreateMap<Item, ItemJson>();
        CreateMap<Addition, AdditionJson>();
        CreateMap<Option, OptionJson>();
    }
}