using System.ComponentModel.DataAnnotations;

namespace SiteManagement.Application.Common.Models.Identity;

public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}