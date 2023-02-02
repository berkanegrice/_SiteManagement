using Microsoft.AspNetCore.Authorization;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Infrastructure.Identity;
using SiteManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteManagement.Application.Common.Interfaces.Due;
using SiteManagement.Application.Common.Interfaces.User;
using SiteManagement.Infrastructure.Persistence.Interceptors;
using SiteManagement.Infrastructure.Services.Applier;
using SiteManagement.Infrastructure.Services.Managements;
using SiteManagement.Infrastructure.Services.Misc;
using SiteManagement.Infrastructure.Services.Permissions;
using SiteManagement.Infrastructure.Services.Registers;
using SiteManagement.Infrastructure.Services.StorageServices;


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
                options.UseSqlite(configuration.GetConnectionString("AppDb"),
                    builder
                        => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Prod_IdentityDb"),
                    builder
                        => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddDatabaseDeveloperPageExceptionFilter();

        #endregion

        #region Feature Services

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IUserFactory, UserFactory>();
        services.AddTransient<IRoleFactory, RoleFactory>();
        services.AddTransient<ISignInFactory, SignInFactory>();
        services.AddTransient<IRegisterFactory, RegisterFactory>();
        services.AddTransient<IApplyService, ApplyService>();

        #endregion

        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}