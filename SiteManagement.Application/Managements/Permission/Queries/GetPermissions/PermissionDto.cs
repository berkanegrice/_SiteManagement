namespace SiteManagement.Application.Managements.Permission.Queries.GetPermissions;

public class PermissionDto
{
    public string RoleId { get; set; }
    public IList<RoleClaimsDto> RoleClaims { get; set; }
}

