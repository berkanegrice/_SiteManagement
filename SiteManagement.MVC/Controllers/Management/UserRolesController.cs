using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Managements.Users.Commands.UpdateUserRoles;
using SiteManagement.Application.Managements.Users.Queries.GetUserRoles;
using SiteManagement.MVC.Models;

namespace SiteManagement.MVC.Controllers.Management;

[Authorize(Roles = "SuperAdmin")]
public class UserRolesController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public UserRolesController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel
            {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
    
    public async Task<IActionResult> Index(string userId)
    {
        var resp = await _mediator.Send(
            new GetUserRoleListQuery()
        {
            UserId = userId
        });

        return View(new ManageUserRolesVM()
        {
            UserId = resp.UserId,
            UserRoles = resp.UserRoles
        });
    }

    public async Task<IActionResult> Update(string id, ManageUserRolesVM model)
    {
        var resp = await _mediator.Send(new UpdateUserRolesCommand()
        {
            Id = id,
            ManageUserRolesDto = new ManageUserRolesDto()
            {
                UserId = model.UserId, UserRoles = model.UserRoles
            },
            User = _currentUserService.User 
        });

        return resp.Status
            ? RedirectToAction("Index", new {userId = id})
            : RedirectToAction("Error");
    }
}