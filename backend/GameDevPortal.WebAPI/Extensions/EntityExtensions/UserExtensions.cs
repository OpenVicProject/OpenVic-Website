using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.ProjectDtos;
using GameDevPortal.WebAPI.Models.Dtos.UserDtos;

namespace GameDevPortal.WebAPI.Extensions.EntityExtensions;

public static class UserExtensions
{
    public static void Update(this User user, UserUpdateDto dto)
    {
        user.SetChangableValues(dto.UserName);
    }
}