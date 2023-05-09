using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.UserDtos;

public class UserAuthenticationDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}