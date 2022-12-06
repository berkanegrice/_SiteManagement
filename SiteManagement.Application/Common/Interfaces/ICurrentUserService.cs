using System.Security.Claims;

namespace SiteManagement.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    ClaimsPrincipal User { get; }
}