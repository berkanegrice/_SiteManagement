using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Infrastructure.Persistence.Seeds;

namespace SiteManagement.Infrastructure.Services.Managements;

public class RoleFactory : IRoleFactory
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleFactory(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> AddRole(string userName)
    {
        return await _roleManager.CreateAsync(new IdentityRole(userName.Trim()));
    }

    public async Task<IdentityRole> FindByIdAsync(string roleId)
    {
        return await _roleManager.FindByIdAsync(roleId);
    }

    public async Task<IList<Claim>> GetClaimsAsync(IdentityRole role)
    {
        return await _roleManager.GetClaimsAsync(role);
    }

    public async Task<IdentityResult> RemoveClaimAsync(IdentityRole role, Claim claim)
    {
        return await _roleManager.RemoveClaimAsync(role, claim);
    }

    public async Task AddPermissionClaim(IdentityRole role, string? claimValue)
    {
        await _roleManager.AddPermissionClaim(role, claimValue!);
    }

    public IQueryable<IdentityRole> Roles => _roleManager.Roles;
}