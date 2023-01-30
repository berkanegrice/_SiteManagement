using Microsoft.AspNetCore.Identity;

namespace SiteManagement.Application.Common.Models.Requests.User;

public class IsInRoleRequest
{
    public IdentityUser User { get; set; }
    public string RoleName { get; set; }
}