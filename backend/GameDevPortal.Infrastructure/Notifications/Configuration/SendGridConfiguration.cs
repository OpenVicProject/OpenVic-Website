using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.Infrastructure.Notifications.Configuration;

public class SendGridConfiguration
{
    public const string SectionName = nameof(SendGridConfiguration);
    [Required]
    public string ApiKey { get; set; }
    [Required]
    public string SenderEmail { get; set; }
    [Required]
    public string SenderName { get; set; }
}