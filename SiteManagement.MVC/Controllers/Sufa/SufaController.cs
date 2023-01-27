using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.SufaRelated.SufaInformations.Queries;
using SiteManagement.Application.SufaRelated.SufaTransactions.Queries;
using SiteManagement.MVC.Services.Sufa;

namespace SiteManagement.MVC.Controllers.Sufa;

public class SufaController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    
    public SufaController(IMediator mediator, ICurrentUserService currentUserService)
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
    public async Task<JsonResult> GetSufasInformation()
    {
        // var value = await _mediator.Send(
        //     new GetSufaInformationQuery { UserId = _currentUserService.UserId });
        // var res = new SufaSorterService(Request.Form).ServerSideSorting(value).Result;
        // return Json(res);
        
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Authorize]
    public async Task<JsonResult> GetSufasTransaction(int userCode)
    {
        // var value = await _mediator.Send(
        //     new GetSufaTransactionQuery{ UserCode = userCode});
        // var res = new SufaTransactionSorterService(Request.Form).ServerSideSorting(value).Result;
        // return Json(res);
        
        throw new NotImplementedException();
    }
}