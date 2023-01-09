using SiteManagement.Application.Managements.Permission.Queries.GetPermissions;

namespace SiteManagement.MVC.Models;

public class PermissionVm
{
    public string RoleId { get; set; }
    public IList<RoleClaimsDto> RoleClaims { get; set; }
}

