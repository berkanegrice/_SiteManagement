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
        // string? message, int? listId, string listName, string listType
        // TempData["Message"] = message;
        // TempData["ListId"] = listId;
        // TempData["ListName"] = listName;
        // TempData["ListType"] = listType;
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

    // [HttpPost]
    // [Authorize(Roles = "SuperAdmin")]
    // public async Task<IActionResult> UploadDueList(
    //     IFormFile file, string description)
    // {
    //     var register = description.Parser();
    //     
    //     var res
    //         = await _mediator.Send(
    //         new UploadDueListCommand()
    //         {
    //             UploadFileCommand = new UploadFileCommand()
    //             {
    //                 File = file,
    //                 Description = register.ToString(),
    //                 UploadedBy = _currentUserService.UserId!
    //             },
    //         });
    //     
    //     return res.Status
    //         ? RedirectToAction("Index", new
    //         {
    //             Message = register.ToString()  + " defteri basariyla yuklendi",
    //             ListId = res.InsertedId, 
    //             ListName = register.RegisterName,
    //             ListType = register.RegisterType
    //         })
    //         : RedirectToAction("Error");
    // }
    
    // [Authorize(Roles = "SuperAdmin")]
    // public async Task<IActionResult> ApplyDueList(int dueListId, string listName, string listType)
    // {
    //     var resp = new ResponseApplyRegisterCommand();
    //     switch (listType)
    //     {
    //         case "Mizan":
    //             switch (listName)
    //             {
    //                 case "Sufa":
    //                     break;
    //                 case "Kidem":
    //                     break;
    //                 default:
    //                     resp = await _mediator.Send(new ApplyDueInfListCommand() {Id = dueListId}); break;
    //             }
    //             break;
    //         case "Muavin":
    //             switch (listName)
    //             {
    //                 case "Sufa":
    //                     break;
    //                 case "Kidem":
    //                     break;
    //                 default:
    //                     resp = await _mediator.Send(new ApplyDueTranListCommand() {Id = dueListId}); break;
    //             }
    //             break;
    //     }
    //     
    //     return resp.Status
    //         ? RedirectToAction("Index", new
    //         {
    //             Message = resp.Type + " defter basariyla uygulandi",
    //             DueListId = resp.InsertedId
    //         })
    //         : RedirectToAction("Error");
    // }
}