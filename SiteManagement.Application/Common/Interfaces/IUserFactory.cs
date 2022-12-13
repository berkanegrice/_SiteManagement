using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Queries.GetFiles;

namespace SiteManagement.Application.Common.Interfaces;

public interface IUserFactory
{
    Task<bool> UploadUserList(UploadFileRequest request);
    Task<bool> ApplyUserList(ApplyUserListRequest request);
}