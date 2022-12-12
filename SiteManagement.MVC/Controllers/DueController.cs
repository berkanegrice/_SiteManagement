using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.DueInformations.Queries.GetDueInformations;
using SiteManagement.Application.DueTransactions.Queries.GetDueTransactions;
using SiteManagement.MVC.Services;

namespace SiteManagement.MVC.Controllers;

public class DueController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    public DueController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public IActionResult Index() => View(); 
    
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