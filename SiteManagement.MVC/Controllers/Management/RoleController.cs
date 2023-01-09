using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Managements.Roles.Commands.AddRole;
using SiteManagement.Application.Managements.Roles.Queries.GetRoles;
using SiteManagement.MVC.Models;

namespace SiteManagement.MVC.Controllers.Management;

public class RoleController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public RoleController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    public async Task<IActionResult> Index()
    {
        var roleList = await _mediator.Send
        (new GetRolesQuery()
        {
            UserId = _currentUserService.UserId
        });
        return View(await roleList.ToListAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> AddRole(string roleName)
    {
        var res = await _mediator.Send(
            new AddRoleCommand()
        {
            RoleName = roleName
        });

        return res.Status
            ? RedirectToAction("Index")
            : RedirectToAction("Error");
    }
}