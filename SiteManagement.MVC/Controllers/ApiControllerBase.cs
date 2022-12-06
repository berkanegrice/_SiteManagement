using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.MVC.Controllers;

public abstract class ApiControllerBase : Controller
{
    private ISender? _mediator = null;
    private ICurrentUserService? _currentUserService = null;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected ICurrentUserService CurrentUserService => _currentUserService ??= HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();
}