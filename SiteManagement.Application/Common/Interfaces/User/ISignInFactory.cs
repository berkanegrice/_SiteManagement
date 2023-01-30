using Microsoft.AspNetCore.Identity;

namespace SiteManagement.Application.Common.Interfaces;

public interface ISignInFactory
{
    Task RefreshSignInAsync(IdentityUser currentUser);
}