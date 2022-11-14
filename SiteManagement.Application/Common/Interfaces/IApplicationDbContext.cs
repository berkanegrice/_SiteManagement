using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.Entities.DuesRelated;
using SiteManagement.Domain.Entities.FileRelated;


namespace SiteManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.DuesRelated.DuesInformation> DuesInformations { get; set; }
    DbSet<UserModel> UsersModel { get; set; }
    DbSet<DuesDetailedInformation> DuesDetailedInformations { get; set; }
    DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    DbSet<FileOnFileSystemModel> FilesOnFileSystem { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}