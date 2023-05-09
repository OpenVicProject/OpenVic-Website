using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.UserDtos;

public class UserRegistrationDto
{
    [Required] 
    public string UserName { get; set; }
    [Required] 
    public string Password { get; set; }
    public string Email { get; set; }
}