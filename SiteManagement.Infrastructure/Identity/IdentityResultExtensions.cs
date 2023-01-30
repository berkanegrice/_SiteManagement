using Microsoft.AspNetCore.Identity;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Misc;

namespace SiteManagement.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}