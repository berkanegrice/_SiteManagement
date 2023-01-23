using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Reports.Commands;
using SiteManagement.Application.Reports.Queries;
using SiteManagement.MVC.Models;

namespace SiteManagement.MVC.Controllers.Report;

public class ReportController : Controller
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator,
        ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public async Task<IActionResult> Index()
    {
        var resp
            = await _mediator.Send(new GetAllReportsQuery());

        return View(await resp.ToListAsync());
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel
            {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    // public async Task<IActionResult> UploadToDatabase(IFormFile file, string description)
    // {
    //     var resp
    //         = await _mediator.Send(
    //             new UploadReportCommand()
    //             {
    //                 UploadFileCommand = new UploadFileCommand()
    //                 {
    //                     File = file,
    //                     Description = "Report." + description,
    //                     UploadedBy = _currentUserService.UserId!
    //                 }
    //             });
    //     return resp.Status
    //         ? RedirectToAction("Index")
    //         : RedirectToAction("Error");
    // }

    public async Task<IActionResult> DownloadFileFromDatabase(int id)
    {
        var respFile = await _mediator.Send(
            new DownloadFileCommand()
        {
            Id = id
        });

        return File(respFile.Data, respFile.FileType, respFile.Name + respFile.Extension);
    }

    public async Task<IActionResult> DeleteFileFromDatabase(int id)
    {
        var resp
            = await _mediator.Send(
                new DeleteFileCommand()
            {
                Id = id
            });

        return resp.Status
            ? RedirectToAction("Index")
            : RedirectToAction("Error");
    }
}