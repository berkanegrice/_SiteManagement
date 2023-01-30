using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.RegisterRelated.RegisterInformations.Queries.GetDueInformations;
using SiteManagement.Application.RegisterRelated.RegisterTransactions.Queries.GetDueTransactions;
using SiteManagement.MVC.Services.Register;


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
            new GetRegisterInformationQuery { UserId = _currentUserService.UserId, Type = "Kidem" });
        var res = new RegisterSorterService(Request.Form).ServerSideSorting(value).Result;
        return Json(res);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<JsonResult> GetKidemsTransaction(int userCode)
    {
        var value = await _mediator.Send(
            new GetRegisterTransactionQuery{ UserCode = userCode});
        var res = new RegisterTransactionSorterService(Request.Form).ServerSideSorting(value).Result;
        return Json(res);
    }
}