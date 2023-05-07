using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.CategoryDtos;
using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.ProgressReportDtos;

public class ProgressReportGetDto
{
    public Guid Id { get; set; }
    [MaxLength(150)]
    public string Title { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public DateTime MadePublicAt { get; set; }
    public Guid ProjectId { get; set; }
    public List<CategoryGetDto> Categories { get; set; }
}
