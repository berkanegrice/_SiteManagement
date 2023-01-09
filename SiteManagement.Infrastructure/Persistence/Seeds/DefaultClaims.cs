using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SiteManagement.Infrastructure.Persistence.Constants;

namespace SiteManagement.Infrastructure.Persistence.Seeds;

public static class DefaultClaims
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
        await roleManager.AddPermissionClaim(adminRole, "LeaseHolder.View");
        await roleManager.AddPermissionClaim(adminRole, "LeaseHolder.Create");
        await roleManager.AddPermissionClaim(adminRole, "LeaseHolder.Edit");
        await roleManager.AddPermissionClaim(adminRole, "LeaseHolder.Delete");
    }

    public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role,
        string module)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        var allPermissions = Permissions.GeneratePermissionsForModule(module);
        foreach (var permission in allPermissions.Where(permission =>
                     !allClaims.Any(a => a.Type == "Permission" && a.Value == permission)))
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        }
    }
}