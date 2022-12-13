using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Users.Queries.GetUsers;
using SiteManagement.MVC.Models;

namespace SiteManagement.MVC.Controllers;

public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserFactory _userFactory;

    public UserController(IMediator mediator, ICurrentUserService currentUserService, 
        IUserFactory userFactory)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        _userFactory = userFactory;
    }
    
    public async Task<IActionResult> Index(string? message)
    {
        TempData["Message"] = message;
        var userList =
            await _mediator.Send(new GetUserListQuery{ UserId = _currentUserService.UserId }); 
        return View(await userList.ToListAsync());
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadUserList(IFormFile file, string description)
    {
        var res = await _userFactory.UploadUserList(new UploadFileRequest()
        {
            FormFile = file,
            Description = description, 
            UploadedBy = _currentUserService.UserId!
        });
        return res
            ? RedirectToAction("Index", new
                { message = "Kullanici listesi basariyla yuklendi" })
            : RedirectToAction("Error");
    }

    public async Task<IActionResult> ApplyUserList(int id)
    {
        var res = await _userFactory.ApplyUserList(new ApplyUserListRequest()
        {
            Id = id
        });
        
        return RedirectToAction("Index");
    }
}