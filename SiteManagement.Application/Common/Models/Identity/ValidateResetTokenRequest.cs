using System.ComponentModel.DataAnnotations;

namespace SiteManagement.Application.Common.Models.Identity;

public class ValidateResetTokenRequest
{
    [Required]
    public string Token { get; set; }
}