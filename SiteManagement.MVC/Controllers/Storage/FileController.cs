using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.DueRelated.DueInformations.Command;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Infrastructure.Services;
using SiteManagement.MVC.Models;

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

    public IActionResult Index()
    {
        var model = new UploadRegisterViewModel();  
        return View(model);  
    }

    //
    // [HttpPost]
    // [Authorize(Roles = "SuperAdmin")]
    // public Task<IActionResult> UploadFile(
    //     IFormFile file)
    // {
    //     return new NotImplementedException();
    // }
    [HttpPost] 
    public IActionResult FileItemDropdown(UploadRegisterViewModel model)
    {
        var EmployeeId = model.EmployeeId;  
        Console.WriteLine(EmployeeId);
        return RedirectToAction("FileItemDropdown");  
    }
}