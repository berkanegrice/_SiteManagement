using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace SiteManagement.Application.Common.Interfaces;

public interface IRoleFactory
{
    Task<IdentityResult> AddRole(string userName);
    Task<IdentityRole> FindByIdAsync(string roleId);
    Task<IList<Claim>> GetClaimsAsync(IdentityRole role);
    Task<IdentityResult> RemoveClaimAsync(IdentityRole role, Claim claim);
    Task AddPermissionClaim(IdentityRole role, string? claimValue);
    IQueryable<IdentityRole> Roles { get; }
}