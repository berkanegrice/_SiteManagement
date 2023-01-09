using System.Reflection;
using SiteManagement.Application.Managements.Permission.Queries.GetPermissions;

namespace SiteManagement.Application.Common.Helper;

public static class ClaimsHelper
{
    public static void GetPermissions(this List<RoleClaimsDto> allPermissions, Type policy, string roleId)
    {
        var fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
        allPermissions.AddRange(fields.Select(fi => new RoleClaimsDto
            {Value = "Permissions."+fi.GetValue(null)!.ToString(), Type = "Permissions"}));
    }
}