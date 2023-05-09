using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.ProjectDtos;

namespace GameDevPortal.WebAPI.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectCreateDto, Project>();
        CreateMap<ProjectCreateDto, ProjectGetDto>();
        CreateMap<Project, ProjectGetDto>();
        CreateMap<Project, ProjectUpdateDto>();
    }
}