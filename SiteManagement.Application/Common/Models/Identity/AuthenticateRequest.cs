using System.ComponentModel.DataAnnotations;

namespace SiteManagement.Application.Common.Models.Identity;

public class AuthenticateRequest
{
    [Required] 
    [EmailAddress] 
    public string Email { get; set; }

    [Required] 
    public string Password { get; set; }
}