using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SiteManagement.MVC.Controllers;

public class FileController : Controller
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Index()
    {
        throw new NotImplementedException();
    }
    
    public IActionResult Error()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> UploadToDatabase(
        List<IFormFile> files, string description)
    {
        throw new NotImplementedException();
       
    }
}