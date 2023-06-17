using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.RegisterRelated.RegisterInformations.Queries.GetDueInformations;
using SiteManagement.Application.RegisterRelated.RegisterTransactions.Queries.GetDueTransactions;
using SiteManagement.MVC.Models;

using SiteManagement.MVC.Services.Register;

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
    public async Task<JsonResult?> GetDuesInformation()
    {
        var value = await _mediator.Send(
            new GetRegisterInformationQuery { UserId = _currentUserService.UserId, Type = "Aidat"});
        var res = new RegisterSorterService(Request.Form).ServerSideSorting(value).Result;
        return Json(res);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<JsonResult> GetDuesTransaction(int userCode)
    {
        var value = await _mediator.Send(
            new GetRegisterTransactionQuery{ UserCode = userCode});
        var res = new RegisterTransactionSorterService(Request.Form).ServerSideSorting(value).Result;
        return Json(res);
    }
}