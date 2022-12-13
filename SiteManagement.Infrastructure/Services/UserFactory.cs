using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;

namespace SiteManagement.Infrastructure.Services;

public class UserFactory : IUserFactory
{
    private readonly IFileService _fileService;
    
    public UserFactory(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    public async Task<bool> UploadUserList(UploadFileRequest request)
    {
        return await _fileService.UploadFile(new UploadFileRequest()
        {
            FormFile = request.FormFile,
            Description = request.Description,
            UploadedBy = request.UploadedBy
        });
    }

    public async Task<bool> ApplyUserList(ApplyUserListRequest request)
    {
        var newUserList = await _fileService.FetchFile(new FetchFileRequest()
        {
            Id = request.Id
        });
        
        return true;
    }
}