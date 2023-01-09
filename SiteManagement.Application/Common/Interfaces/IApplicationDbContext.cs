using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.Entities.DuesRelated;
using SiteManagement.Domain.Entities.FileRelated;


namespace SiteManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<DueTransaction> DueTransactions { get; set; }
    DbSet<DueInformation> DueInformations { get; set; }
    DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}