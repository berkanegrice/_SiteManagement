using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Managements.Users.Commands.ApplyUser;
using SiteManagement.Application.Managements.Users.Commands.UploadUser;
using SiteManagement.Application.Managements.Users.Queries.GetUsers;
using SiteManagement.MVC.Models;

namespace SiteManagement.MVC.Controllers.Management;

[Authorize(Roles = "SuperAdmin")]
public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public UserController(IMediator mediator, 
        ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public async Task<IActionResult> Index(string? message, int userListId)
    {
        TempData["Message"] = message;
        TempData["UserListId"] = userListId;
        var userList =
            await _mediator.Send(
                new GetUserListQuery { UserId = _currentUserService.UserId });
        return View(await userList.ToListAsync());
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel
            {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    [HttpPost]
    public async Task<IActionResult> UploadUserList(IFormFile file, string description)
    {
        var res = await _mediator.Send(
            new UploadUserListCommand()
        {
            UploadFileCommand = new UploadFileCommand()
            {
                File = file,
                Description = description,
                UploadedBy = _currentUserService.UserId!
            }
        });
        
        return res!.Status
            ? RedirectToAction("Index", new
            {
                Message = "Kullanici listesi basariyla yuklendi",
                UserListId = res.InsertedId
            })
            : RedirectToAction("Error");
    }

    public async Task<IActionResult> ApplyUserList(int userListId)
    {
        var res = await _mediator.Send(
            new ApplyUserListCommand()
        {
            Id = userListId
        });
        
        return res.Status
            ? RedirectToAction("Index")
            : RedirectToAction("Error");
    }
}