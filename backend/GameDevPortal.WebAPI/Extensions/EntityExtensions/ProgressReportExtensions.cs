using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.ProgressReportDtos;

namespace GameDevPortal.WebAPI.Extensions.EntityExtensions;

public static class ProgressReportExtensions
{
    public static void Update(this ProgressReport progressReport, ProgressReportUpdateDto dto)
    {
        progressReport.SetChangableValues(dto.Title, dto.Description, dto.MadePublicAt, dto.CategoryIds);
    }
}