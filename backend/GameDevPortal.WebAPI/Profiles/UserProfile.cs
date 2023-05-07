using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.WebAPI.Models.Dtos.UserDtos;

namespace GameDevPortal.WebAPI.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationDto, User>();
        CreateMap<UserCreationDto, User>();
        CreateMap<User, UserGetDto>();
    }
}