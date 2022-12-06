using Microsoft.AspNetCore.Identity;
using SiteManagement.Infrastructure.Persistence.Constants;

namespace SiteManagement.Infrastructure.Persistence.Seeds;

public static class DefaultUsers
{
    public static async Task SeedAsync(UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        await SeedDummyUsersAsync(userManager);
        // await SeedFromUserListAsync(userManager, roleManager);
    }

    private static async Task SeedDummyUsersAsync(UserManager<IdentityUser> userManager)
    {
        var defaultUser = new IdentityUser
        {
            UserName = "basicuser@gmail.com",
            Email = "basicuser@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
            }
        }
        
        var superUser = new IdentityUser
        {
            UserName = "superadmin@gmail.com",
            Email = "superadmin@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        if (userManager.Users.All(u => u.Id != superUser.Id))
        {
            var user = await userManager.FindByEmailAsync(superUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(superUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(superUser, Roles.Basic.ToString());
                await userManager.AddToRoleAsync(superUser, Roles.Admin.ToString());
                await userManager.AddToRoleAsync(superUser, Roles.SuperAdmin.ToString());
            }
        }
    }
    
    private static async Task SeedFromUserListAsync(UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        throw new NotImplementedException();
    }
}