using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.MVC.Models;
using MediatR;
using SiteManagement.Application.Files.Commands.ApplyFiles;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Reports.Commands;
using SiteManagement.Domain.Entities.Enums;

namespace SiteManagement.MVC.Controllers.Storage;

public class FileController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public FileController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public IActionResult Index(string? message, int? insertedId)
    {
        TempData["Message"] = message;
        TempData["InsertedId"] = insertedId;
        var model = new UploadRegisterViewModel();
        return View(model);
    }

    public IActionResult Error() => View(new ErrorViewModel
        {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});

    public async Task<IActionResult> UploadToDatabase(IFormFile file,
        string description, RegisterType? registerType, RegisterName? registerName)
    {
        var resp = await _mediator.Send(new UploadFileCommand()
        {
            File = file,
            Description = description,
            FileType = $"{registerName.ToString()} {registerType.ToString()}".TrimEnd(),
            UploadedBy = _currentUserService.UserId!
        });

        return resp.Status
            ? RedirectToAction("Index", new
            {
                Message = $"{registerName.ToString()} {registerType.ToString()} eklendi",
                InsertedId = resp.InsertedId
            })
            : RedirectToAction("Error");
    }

    public async Task<IActionResult> ApplyList(int insertedId)
    {
        var resp = await _mediator.Send(new ApplyListCommand() { Id = insertedId });
             
        return resp.Status
            ? RedirectToAction("Index", new
            {
                Message = resp.Type,
                DueListId = resp.InsertedId
            })
            : RedirectToAction("Error");
    }
    
    public async Task<IActionResult> DownloadFileFromDatabase(int id)
    {
        var respFile = await _mediator.Send(new DownloadFileCommand() { Id = id });
        
        return File(respFile.Data, respFile.FileType, respFile.Name + respFile.Extension);
    }

    public async Task<IActionResult> DeleteFileFromDatabase(int id)
    {
        var resp = await _mediator.Send(new DeleteFileCommand() {Id = id});
        
        return resp.Status
            ? RedirectToAction("Index")
            : RedirectToAction("Error");
    }
}