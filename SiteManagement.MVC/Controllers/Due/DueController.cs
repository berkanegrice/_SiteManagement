using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.DueRelated.DueInformations.Command;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;
using SiteManagement.Application.DueRelated.DueInformations.Response;
using SiteManagement.Application.DueRelated.DueTransactions.Commands;
using SiteManagement.Application.DueRelated.DueTransactions.Queries.GetDueTransactions;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.MVC.Models;
using SiteManagement.MVC.Services;

using SiteManagement.Infrastructure.Services;
using SiteManagement.MVC.Services.Due;

namespace SiteManagement.MVC.Controllers.Due;

public class DueController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public DueController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error() => View(new ErrorViewModel
        {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    
    [HttpPost]
    [Authorize]
    public async Task<JsonResult> GetDuesInformation()
    {
        var value = await _mediator.Send(
            new GetDueInformationQuery { UserId = _currentUserService.UserId });
        var res = new DueSorterService(Request.Form).ServerSideSorting(value).Result;
        return Json(res);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<JsonResult> GetDuesTransaction(int userCode)
    {
        var value = await _mediator.Send(
            new GetDueTransactionQuery{ UserCode = userCode});
        var res = new DueTransactionSorterService(Request.Form).ServerSideSorting(value).Result;
        return Json(res);
    }
}