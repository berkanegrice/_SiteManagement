using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Files.Queries.GetFiles;
using SiteManagement.Application.Users.Commands;

namespace SiteManagement.Application.Common.Interfaces;

public interface IUserFactory
{
    Task<ResponseUploadUserListCommand> UploadUserList(UploadFileRequest request);
    Task<ResponseApplyUserListCommand> ApplyUserList(ApplyUserListRequest request);
}