using System.Security.Claims;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.MVC.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User!;
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}