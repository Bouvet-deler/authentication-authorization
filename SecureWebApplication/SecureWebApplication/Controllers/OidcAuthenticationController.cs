using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace SecureWebApplication.Controllers;

public class OidcAuthenticationController : Controller
{
    [HttpGet("Account/Logout")]
    public IActionResult LogoutAsync()
    {
        var authenticationProperties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("Index", "Home"),
            Parameters = { { "client_id", "securewebapplication" } }
        };
        return SignOut(authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
    }

    [HttpGet("Account/AccessDenied")]
    public IActionResult AccessDenied(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View("AccessDenied");
    }
}