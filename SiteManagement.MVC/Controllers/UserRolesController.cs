using Microsoft.AspNetCore.Mvc;

namespace SiteManagement.MVC.Controllers;

public class UserRolesController : Controller
{
    public async Task<IActionResult> Index(string userId)
    {
        return View();
    }
}