using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.KidemRelated.KidemInformations.Queries;
using SiteManagement.Application.KidemRelated.KidemTransactions.Queries;
using SiteManagement.MVC.Services.Kidem;

namespace SiteManagement.MVC.Controllers.Kidem;

public class KidemController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    
    public KidemController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize]
    public async Task<JsonResult> GetKidemsInformation()
    {
        var value = await _mediator.Send(
            new GetKidemInformationQuery { UserId = _currentUserService.UserId });
        var res = new KidemSorterService(Request.Form).ServerSideSorting(value).Result;
        return Json(res);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<JsonResult> GetKidemsTransaction(int userCode)
    {
        var value = await _mediator.Send(
            new GetKidemTransactionQuery{ UserCode = userCode});
        var res = new KidemTransactionSorterService(Request.Form).ServerSideSorting(value).Result;
        return Json(res);
    }
}