using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureWebApplication.Models;
using System.Diagnostics;

namespace SecureWebApplication.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewModel = GetHomeViewModel();
        return View(viewModel);
    }

    [Authorize]
    public IActionResult Secure()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private HomeViewModel GetHomeViewModel()
    {
        var viewModel = new HomeViewModel
        {
            IsAuthenticated = User.Identity?.IsAuthenticated == true,
            UserName = User.Identity?.Name
        };

        if (User.Identity?.IsAuthenticated == true)
        {
            foreach (var userClaim in User.Claims)
            {
                viewModel.Claims.Add(new ClaimViewModel { Type = userClaim.Type, Value = userClaim.Value });
            }
        }

        return viewModel;
    }
}

public class HomeViewModel
{
    public bool IsAuthenticated { get; set; }
    public string? UserName { get; set; }
    public List<ClaimViewModel> Claims { get; } = new();
}

public class ClaimViewModel
{
    public string? Type { get; set; }
    public string? Value { get; set; }
}