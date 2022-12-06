using System.ComponentModel.DataAnnotations;

namespace SiteManagement.Application.Common.Models.Identity;

public class VerifyEmailRequest
{
    [Required]
    public string Token { get; set; }
}