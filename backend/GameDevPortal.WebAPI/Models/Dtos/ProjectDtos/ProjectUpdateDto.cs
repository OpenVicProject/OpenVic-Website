using GameDevPortal.Core.Entities;
using GameDevPortal.Core.ValueTypes;
using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.ProjectDtos;

public class ProjectUpdateDto
{
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public ProjectTimeFrame TimeFrame { get; set; }
}