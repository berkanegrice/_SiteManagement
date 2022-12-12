using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Users.Queries.GetUsers;

namespace SiteManagement.MVC.Controllers;

public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public UserController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }
    
    public async Task<IActionResult> Index()
    {
        var userList =
            await _mediator.Send(new GetUserListQuery{ UserId = _currentUserService.UserId }); 
        return View(await userList.ToListAsync());
    }
}