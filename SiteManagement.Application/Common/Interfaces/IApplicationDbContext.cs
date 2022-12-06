using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.Entities.DuesRelated;
using SiteManagement.Domain.Entities.FileRelated;


namespace SiteManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<DueInformation> DueInformations { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<DueTransaction> DueTransactions { get; set; }
    DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    DbSet<FileOnFileSystemModel> FilesOnFileSystem { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}