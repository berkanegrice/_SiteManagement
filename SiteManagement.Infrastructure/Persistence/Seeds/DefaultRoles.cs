using Microsoft.AspNetCore.Identity;
using SiteManagement.Infrastructure.Persistence.Constants;

namespace SiteManagement.Infrastructure.Persistence.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
    }
}