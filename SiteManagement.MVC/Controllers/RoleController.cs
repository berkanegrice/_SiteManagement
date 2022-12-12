using Microsoft.AspNetCore.Mvc;

namespace SiteManagement.MVC.Controllers;

public class RoleController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}