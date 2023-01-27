using System.Reflection;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.Entities.FileRelated;
using SiteManagement.Domain.Entities.RegisterRelated;
using SiteManagement.Infrastructure.Common;

namespace SiteManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
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
            .HasMany(t => t.RegisterInformations)
            .WithMany(t => t.Users)
            .UsingEntity(j => j.ToTable("UserRegister"));
        
        builder.Entity<RegisterInformation>()
            .HasMany(di => di.RegisterTransactions)
            .WithOne(di => di.RegisterInformation)
            .HasForeignKey(dt => dt.AccountCode);
        
        
        // builder.Entity<User>()
        //     .HasMany<RegisterInformation>(s => s.RegisterInformations)
        //     .WithMany(c => c.Users)
        //     .Map(cs =>
        //     {
        //         cs.MapLeftKey("UserRefId");
        //         cs.MapRightKey("RegisterInformationRefId");
        //         cs.ToTable("UserRegister");
        //     });
        
        
        // builder.Entity<Student>()
        //     .HasMany<Course>(s => s.Courses)
        //     .WithMany(c => c.Students)
        //     .Map(cs =>
        //     {
        //         cs.MapLeftKey("StudentRefId");
        //         cs.MapRightKey("CourseRefId");
        //         cs.ToTable("StudentCourse");
        //     });
        
        // builder.Entity<RegisterInformation>()
        //     .HasOne<User>(s => s.User)
        //     .WithMany(g => g.RegisterInformations)
        //     .HasForeignKey(s => s.UserRegisterId);
        //

        
        
        
        // builder.Entity<User>()
        //     .HasOne(u => u.Due)
        //     .WithOne(d => d.User)
        //     .HasPrincipalKey<User>(u => u.UserCode)
        //     .HasForeignKey<DueInformation>(d => d.AccountCode);
        //
        // builder.Entity<DueInformation>()
        //     .HasMany(di => di.Transactions)
        //     .WithOne(di => di.DueInformation)
        //     .HasPrincipalKey(di=> di.AccountCode)
        //     .HasForeignKey(dt => dt.AccountCode);
    
        // builder.ApplyConfiguration(new RoleConfiguration());
        // builder.ApplyConfiguration(new UserConfiguration());
        // builder.ApplyConfiguration(new UserRoleConfiguration());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<RegisterTransaction> RegisterTransactions { get; set; }
    
    public DbSet<RegisterInformation> RegisterInformations { get; set; }
    public DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
    
        return await base.SaveChangesAsync(cancellationToken);
    }
}