using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.FaqDtos;

public class FaqUpdateDto
{
    [MaxLength(150)]
    public string Question { get; set; }
    [MaxLength(1500)]
    public string Answer { get; set; }
}