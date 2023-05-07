using GameDevPortal.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.ProgressReportDtos;

public class ProgressReportCreateDto
{
    [MaxLength(150)]
    public string Title { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public DateTime MadePublicAt { get; set; }
    public Guid ProjectId { get; set; }
    public List<Guid> CategoryIds { get; set; }
}
