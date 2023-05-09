using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.UserDtos;

public class UserCreationDto
{
    [Required] 
    public string UserName { get; set; }
    [Required] 
    public string Password { get; set; }
    public string Email { get; set; }
    public ICollection<string> Roles { get; set; }
}