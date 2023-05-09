using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.CategoryDtos;

public class CategoryUpdateDto
{
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(7)]
    public string HexColour { get; set; }

    public Guid? ProjectId { get; set; }
}