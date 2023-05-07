using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.ProgressReportDtos;

namespace GameDevPortal.WebAPI.Profiles;

public class ProgressReportProfile : Profile
{
    public ProgressReportProfile()
    {
        CreateMap<ProgressReportCreateDto, ProgressReport>().ConstructUsing(s => new ProgressReport(s.Title, s.Description, s.MadePublicAt, s.ProjectId, s.CategoryIds)).ForMember(dest => dest.CategoryIds, opt => opt.Ignore());
        CreateMap<ProgressReportCreateDto, ProgressReportGetDto>();
        CreateMap<ProgressReport, ProgressReportGetDto>();
        CreateMap<ProgressReport, ProgressReportUpdateDto>();
    }
}