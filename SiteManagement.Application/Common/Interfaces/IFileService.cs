using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Files.Queries.GetFiles;
using SiteManagement.Application.Reports.Commands;

namespace SiteManagement.Application.Common.Interfaces;

public interface IFileService
{
    Task<ResponseUploadFileCommand> UploadFile(UploadFileRequest request);
    Task<FileOnDataBaseDto> FetchFileById(FetchFileRequest request);
    Task<IQueryable<FileOnDataBaseDto>> FetchFileByCategory(FetchFileRequest request);
    Task<ResponseDeleteFileCommand> DeleteFileById(DeleteFileRequest request);
}