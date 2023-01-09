using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Managements.Permission.Commands;
using SiteManagement.Application.Managements.Permission.Queries.GetPermissions;
using SiteManagement.MVC.Models;

namespace SiteManagement.MVC.Controllers.Management;

[Authorize(Roles = "SuperAdmin")]
public class PermissionController : Controller
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel
            {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
    public async Task<ActionResult> Index(string roleId)
    {
        var respModel = await _mediator.Send(
            new GetPermissionListQuery()
            {
                RoleId = roleId
            });

        return View(new PermissionVm()
        {
            RoleClaims = respModel.RoleClaims,
            RoleId = respModel.RoleId
        });
    }

    public async Task<IActionResult> Update(PermissionVm request)
    {
        var resp = await _mediator.Send(new PermissionUpdateCommand()
        {
            PermissionDto = new PermissionDto()
            {
                RoleClaims = request.RoleClaims,
                RoleId = request.RoleId
            }
        });

        return resp.Status
            ? RedirectToAction("Index", new {roleId = request.RoleId})
            : RedirectToAction("Error");
    }
}