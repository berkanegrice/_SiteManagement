using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Interfaces.User;
using SiteManagement.Application.Common.Models.Requests.File;
using SiteManagement.Application.Common.Models.Requests.User;
using SiteManagement.Application.Managements.Users.Commands.ApplyUser;
using SiteManagement.Application.Managements.Users.Commands.UploadUser;
using SiteManagement.Domain.Entities;
using SiteManagement.Infrastructure.Services.CsvReaderHelper;

namespace SiteManagement.Infrastructure.Services.Managements;

public class UserFactory : IUserFactory
{
    private readonly IFileService _fileService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IApplicationDbContext _context;

    public UserFactory(IFileService fileService,
        UserManager<IdentityUser> userManager,
        IApplicationDbContext context)
    {
        _fileService = fileService;
        _userManager = userManager;
        _context = context;
    }

    public async Task<ResponseUploadUserListCommand> UploadUserList(UploadFileRequest request)
    {
        var res = await _fileService.UploadFile(new UploadFileRequest()
        {
            File = request.File,
            Description = request.Description,
            UploadedBy = request.UploadedBy
        });

        return new ResponseUploadUserListCommand
        {
            Status = res.Status,
            InsertedId = res.InsertedId
        };
    }

    public async Task<ResponseApplyUserListCommand> ApplyUserList(ApplyUserListRequest request)
    {
        #region Fetch Data
        
        var newUserListDto = await _fileService
            .FetchFileById(new FetchFileRequest()
            {
                Id = request.Id
            });

        var usersOnCsv = Serializer<UserOnCsv>
            .Deserialize(newUserListDto.Data);

        #endregion
        
        #region Add to IdentityUser Table
        
        // foreach (var userOnCsv in usersOnCsv)
        // {
        //     var us = new UserModel(userOnCsv);
        //     var defaultUser = new IdentityUser
        //     {
        //         UserName = us.Email,
        //         Email = us.Email,
        //         PhoneNumber = us.PhoneNumber,
        //         EmailConfirmed = true,
        //         PhoneNumberConfirmed = true,
        //     };
        //
        //     if (!_userManager.Users.All(u => u.Id != defaultUser.Id)) continue;
        //     var user = await _userManager.FindByEmailAsync(defaultUser.Email);
        //     if (user != null) continue;
        //     await _userManager.CreateAsync(defaultUser, "123Pa$$word!");
        //     await _userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
        // }
        //
        #endregion
        
        #region Add to Users table
        
        foreach (var userOnCsv in usersOnCsv)
        {
            var us = new UserModel(userOnCsv);
            
            // "Aidat" => 13101000,
            // "Sufa" => 13201000,
            // "Kidem" => 13301000,
            
            _context.Users.Add(new User()
            {
                UserName = us.UserName,
                AccountCode = int.Parse("131" + us.UserCode),
                Type = "Aidat",
                PhoneNumber = us.PhoneNumber,
                Email = us.Email,
                Address = us.Address,
                Created = DateTime.Now
            });
            
            _context.Users.Add(new User()
            {
                UserName = us.UserName,
                AccountCode = int.Parse("132" + us.UserCode),
                Type = "Sufa",
                PhoneNumber = us.PhoneNumber,
                Email = us.Email,
                Address = us.Address,
                Created = DateTime.Now
            });
            
            _context.Users.Add(new User()
            {
                UserName = us.UserName,
                AccountCode = int.Parse("133" + us.UserCode),
                Type = "Kidem",
                PhoneNumber = us.PhoneNumber,
                Email = us.Email,
                Address = us.Address,
                Created = DateTime.Now
            });
        }
        
        await _context.SaveChangesAsync(default);
        
        #endregion
        
        return new ResponseApplyUserListCommand()
        {
            Status = true
        };
    }

    public async Task<IdentityUser> FindByIdAsync(FindByIdRequest request)
    {
        return await _userManager.FindByIdAsync(request.UserId);
    }
    
    public async Task<bool> IsInRoleAsync(IsInRoleRequest request)
    {
        return await _userManager.IsInRoleAsync(request.User, request.RoleName);
    }

    public async Task<IList<string>> GetRolesAsync(IdentityUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<IdentityResult> RemoveFromRolesAsync(IdentityUser user, IList<string> roles)
    {
        return await _userManager.RemoveFromRolesAsync(user, roles);
    }

    public async Task<IdentityResult> AddToRolesAsync(IdentityUser user, IEnumerable<string> select)
    {
        return await _userManager.AddToRolesAsync(user, select);
    }

    public async Task<IdentityUser> GetUserAsync(ClaimsPrincipal user)
    {
        return await _userManager.GetUserAsync(user);
    }
}