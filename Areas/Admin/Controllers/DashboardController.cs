using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public DashboardController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
