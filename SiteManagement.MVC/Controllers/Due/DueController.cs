using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.DueRelated.DueInformations.Command;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;
using SiteManagement.Application.DueRelated.DueTransactions.Commands;
using SiteManagement.Application.DueRelated.DueTransactions.Queries.GetDueTransactions;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.MVC.Models;
using SiteManagement.MVC.Services;

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

    public IActionResult Index(string? message, int? dueListId, string dueListType)
    {
        TempData["Message"] = message;
        TempData["DueListId"] = dueListId;
        TempData["DueListType"] = dueListType;
        return View();
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel
            {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
    
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

    [HttpPost]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> UploadDueList(
        IFormFile file, string description)
    {
        var dueType = description.Contains("Mizan") ? "Mizan" : "Muavin";
        var res
            = await _mediator.Send(
            new UploadDueListCommand()
            {
                UploadFileCommand = new UploadFileCommand()
                {
                    File = file,
                    Description = description,
                    UploadedBy = _currentUserService.UserId!
                },
            });
        
        return res.Success
            ? RedirectToAction("Index", new
            {
                Message = dueType + " defteri basariyla yuklendi",
                DueListId = res.InsertedId, 
                DueListType = dueType
            })
            : RedirectToAction("Error");
    }
    
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> ApplyDueList(int dueListId, string dueListType)
    {
        var resp = dueListType switch
        {
            "Mizan" => await _mediator.Send(new ApplyDueInfListCommand() {Id = dueListId}),
            "Muavin" => await _mediator.Send(new ApplyDueTranListCommand() {Id = dueListId}),

            _ => throw new ArgumentOutOfRangeException(nameof(dueListType), dueListType, null)
        };

        return resp.Status
            ? RedirectToAction("Index", new
            {
                Message = resp.Type + " defter basariyla uygulandi",
                DueListId = resp.InsertedId
            })
            : RedirectToAction("Error");
    }
}