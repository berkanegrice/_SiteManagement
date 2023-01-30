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

    public async Task<IActionResult> Index(string? message, int insertedId)
    {
        TempData["Message"] = message;
        TempData["InsertedId"] = insertedId;
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
        const string registerName = "UserList";
        var resp = await _mediator.Send(new UploadFileCommand()
        {
            File = file,
            Description = description,
            FileType = registerName,
            UploadedBy = _currentUserService.UserId!
        });

        return resp.Status
            ? RedirectToAction("Index", new
            {
                Message = "Kullanici listesi eklendi",
                InsertedId = resp.InsertedId
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