using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Twest2.Areas.Identity.Data;
using Twest2.Models;

namespace Twest2.Controllers;

//this attribute tells that this class can only be visible for logged in users
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    
    //add userManager to expose logged-in user data to home page
    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        this._userManager = userManager;
    }

    public IActionResult Index()
    {
        //this.User -> saved during login operation
        ViewData["UserID"] = _userManager.GetUserId(this.User);
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

