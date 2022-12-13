using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Queries.GetFiles;

namespace SiteManagement.Application.Common.Interfaces;

public interface IFileService
{
    Task<bool> UploadFile(UploadFileRequest request);
    Task<FileOnDataBaseDto> FetchFile(FetchFileRequest request);
}