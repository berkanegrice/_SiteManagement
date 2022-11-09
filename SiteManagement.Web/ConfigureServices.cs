using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Infrastructure.Persistence;
using SiteManagement.Web.Services;

namespace SiteManagement.Web;

public static class ConfigureServices
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });                
        });
        
        services.AddControllers();
        
        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);
        
        return services;
    }
}