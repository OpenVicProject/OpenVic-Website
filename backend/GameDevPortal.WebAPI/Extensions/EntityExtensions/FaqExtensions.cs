using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.FaqDtos;

namespace GameDevPortal.WebAPI.Extensions.EntityExtensions;

public static class FaqExtensions
{
    public static void Update(this Faq faq, FaqUpdateDto dto)
    {
        faq.SetChangableValues(dto.Question, dto.Answer);
    }
}