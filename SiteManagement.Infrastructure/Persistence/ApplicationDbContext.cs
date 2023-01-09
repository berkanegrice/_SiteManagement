using System.Reflection;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.Entities.DuesRelated;
using SiteManagement.Domain.Entities.FileRelated;
using SiteManagement.Infrastructure.Common;

namespace SiteManagement.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, 
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor 
    ) 
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
        base.OnModelCreating(builder);
    
        builder.Entity<User>()
            .HasOne(u => u.Due)
            .WithOne(d => d.User)
            .HasPrincipalKey<User>(u => u.UserCode)
            .HasForeignKey<DueInformation>(d => d.AccountCode);
    
        builder.Entity<DueInformation>()
            .HasMany(di => di.Transactions)
            .WithOne(di => di.DueInformation)
            .HasPrincipalKey(di=> di.AccountCode)
            .HasForeignKey(dt => dt.AccountCode);
    
        // builder.ApplyConfiguration(new RoleConfiguration());
        // builder.ApplyConfiguration(new UserConfiguration());
        // builder.ApplyConfiguration(new UserRoleConfiguration());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
    public new DbSet<User> Users { get; set; }
    public DbSet<DueTransaction> DueTransactions { get; set; }
    public DbSet<DueInformation> DueInformations { get; set; }
    public DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
    
        return await base.SaveChangesAsync(cancellationToken);
    }
}