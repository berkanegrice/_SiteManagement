using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Managements.Users.Commands;
using SiteManagement.Application.Managements.Users.Commands.ApplyUser;
using SiteManagement.Application.Managements.Users.Commands.UploadUser;

namespace SiteManagement.Application.Common.Interfaces;

public interface IUserFactory
{
    Task<ResponseUploadUserListCommand> UploadUserList(UploadFileRequest request);
    Task<ResponseApplyUserListCommand> ApplyUserList(ApplyUserListRequest request);
    Task<IdentityUser> FindByIdAsync(FindByIdRequest request);
    Task<bool> IsInRoleAsync(IsInRoleRequest request);
    Task<IList<string>> GetRolesAsync(IdentityUser user);
    Task<IdentityResult> RemoveFromRolesAsync(IdentityUser user, IList<string> roles);
    Task<IdentityResult> AddToRolesAsync(IdentityUser user, IEnumerable<string> select);
    Task<IdentityUser> GetUserAsync(ClaimsPrincipal user);
}