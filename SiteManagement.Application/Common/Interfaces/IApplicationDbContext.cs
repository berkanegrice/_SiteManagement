using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities.FileRelated;
using SiteManagement.Domain.Entities.RegisterRelated;


namespace SiteManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.User> Users { get; set; }
    DbSet<RegisterTransaction> RegisterTransactions { get; set; }
    DbSet<RegisterInformation> RegisterInformations { get; set; }
    DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}