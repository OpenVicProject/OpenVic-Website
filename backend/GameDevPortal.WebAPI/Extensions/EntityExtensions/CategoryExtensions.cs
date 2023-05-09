using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.CategoryDtos;

namespace GameDevPortal.WebAPI.Extensions.EntityExtensions;

public static class CategoryExtensions
{
    public static void Update(this Category category, CategoryUpdateDto dto)
    {
        category.SetChangableValues(dto.Name, dto.HexColour);
    }
}