using SiteManagement.Application.Common.Interfaces;
using SiteManagement.MVC.Services;
using SiteManagement.MVC.Services.Misc;

namespace SiteManagement.MVC;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUiServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        
        return services;
    }
}