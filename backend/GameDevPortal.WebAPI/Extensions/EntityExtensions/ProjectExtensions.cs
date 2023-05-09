using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.ProjectDtos;

namespace GameDevPortal.WebAPI.Extensions.EntityExtensions;

public static class ProjectExtensions
{
    public static void Update(this Project project, ProjectUpdateDto dto)
    {
        project.SetChangableValues(dto.Name, dto.Description, dto.TimeFrame);
    }
}