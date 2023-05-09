using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Models.Dtos.UserDtos;

public class UserGetDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}