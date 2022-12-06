using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.DueDetailedInformations.Queries.GetDueDetailedInformations;
using SiteManagement.Application.DueInformations.Queries.GetDueInformations;

namespace SiteManagement.MVC.Controllers;

public class DueController : ApiControllerBase
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    [Authorize]
    public async Task<JsonResult> GetDuesInformation()
    {
        var value = await Mediator.Send(
            new GetDueInformationQuery{ UserId = CurrentUserService.UserId });
        
        // var draw = Request.Form["draw"].FirstOrDefault();
        var jsonData = new { recordsFiltered = value.Lists, recordsTotal = value.Lists.Count, data = value.Lists };
        
        return Json(jsonData);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<JsonResult> GetDuesDetailedInformation(int rowId)
    {
        var value = await Mediator.Send(
            new GetDueDetailedInformationQuery{ RowId = rowId});
        return Json(value.Lists);
    }
}