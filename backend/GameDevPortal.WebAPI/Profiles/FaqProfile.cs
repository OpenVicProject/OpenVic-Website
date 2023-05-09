using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.FaqDtos;

namespace GameDevPortal.WebAPI.Profiles;

public class FaqProfile : Profile
{
    public FaqProfile()
    {
        CreateMap<FaqCreateDto, Faq>();
        CreateMap<FaqCreateDto, FaqGetDto>();
        CreateMap<Faq, FaqGetDto>();
        CreateMap<Faq, FaqUpdateDto>();
    }
}