using Microsoft.AspNetCore.Authorization;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Infrastructure.Identity;
using SiteManagement.Infrastructure.Persistence;
using SiteManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteManagement.Infrastructure.Persistence.Interceptors;
using SiteManagement.Infrastructure.Services.Permissions;


namespace SiteManagement.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {       
        #region Database Configuration
        
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanArchitectureDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DevelopmentDb"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddDatabaseDeveloperPageExceptionFilter();

        #endregion

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IUserFactory, UserFactory>();

        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}