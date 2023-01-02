using Microsoft.AspNetCore.Identity;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Users.Commands;
using SiteManagement.Infrastructure.Persistence.Constants;
using SiteManagement.Infrastructure.Services.CsvReaderHelper;

namespace SiteManagement.Infrastructure.Services;

public class UserFactory : IUserFactory
{
    private readonly IFileService _fileService;
    private readonly UserManager<IdentityUser> _userManager;

    public UserFactory(IFileService fileService,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _fileService = fileService;
        _userManager = userManager;
    }

    public async Task<ResponseUploadUserListCommand> UploadUserList(UploadFileRequest request)
    {
        var res = await _fileService.UploadFile(new UploadFileRequest()
        {
            FormFile = request.FormFile,
            Description = request.Description,
            UploadedBy = request.UploadedBy
        });

        return new ResponseUploadUserListCommand
        {
            Success = res.Success,
            InsertedId = res.InsertedId
        };
    }

    public async Task<ResponseApplyUserListCommand> ApplyUserList(ApplyUserListRequest request)
    {
        #region Fetch Data

        var newUserListDto = await _fileService
            .FetchFile(new FetchFileRequest()
            {
                Id = request.Id
            });
        
        #endregion
        
        var usersOnCsv = Serializer<UserOnCsv>
            .Deserialize(newUserListDto.Data);

        foreach (var userOnCsv in usersOnCsv)
        {
            var us = new UserModel(userOnCsv);
            var defaultUser = new IdentityUser
            {
                UserName = us.Email,
                Email = us.Email,
                PhoneNumber = us.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (!_userManager.Users.All(u => u.Id != defaultUser.Id)) continue;
            var user = await _userManager.FindByEmailAsync(defaultUser.Email);
            if (user != null) continue;
            await _userManager.CreateAsync(defaultUser, "123Pa$$word!");
            await _userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
        }
        return new ResponseApplyUserListCommand()
        {
            Status = true
        };
    }
}