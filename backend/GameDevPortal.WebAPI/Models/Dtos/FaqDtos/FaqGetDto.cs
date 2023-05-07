using GameDevPortal.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.FaqDtos;

public class FaqGetDto
{
    public Guid Id { get; set; }
    [MaxLength(150)]
    public string Question { get; set; }
    [MaxLength(1500)]
    public string Answer { get; set; }

    public Guid ProjectId { get; set; }
}