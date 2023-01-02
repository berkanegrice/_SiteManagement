using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Files.Queries.GetFiles;

namespace SiteManagement.Application.Common.Interfaces;

public interface IFileService
{
    Task<ResponseUploadFileCommand> UploadFile(UploadFileRequest request);
    Task<FileOnDataBaseDto> FetchFile(FetchFileRequest request);
}