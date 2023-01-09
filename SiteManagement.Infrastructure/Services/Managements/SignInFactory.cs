using Microsoft.AspNetCore.Identity;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Infrastructure.Services.Managements;

public class SignInFactory : ISignInFactory
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public SignInFactory(SignInManager<IdentityUser> signInFactory)
    {
        _signInManager = signInFactory;
    }
    
    public async Task RefreshSignInAsync(IdentityUser currentUser)
    {
        await _signInManager.RefreshSignInAsync(currentUser);
    }
}