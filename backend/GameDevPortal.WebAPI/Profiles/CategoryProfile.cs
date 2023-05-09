using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.CategoryDtos;

namespace GameDevPortal.WebAPI.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryCreateDto, CategoryGetDto>();
        CreateMap<Category, CategoryGetDto>();
        CreateMap<Category, CategoryUpdateDto>();
    }
}