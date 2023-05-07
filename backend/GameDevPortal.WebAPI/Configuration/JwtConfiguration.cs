using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.WebAPI.Configuration;

public class JwtConfiguration
{
    public const string SectionName = "JwtSettings";

    [Required]
    public string ValidIssuer { get; set; }

    [Required]
    public string ValidAudience { get; set; }
}