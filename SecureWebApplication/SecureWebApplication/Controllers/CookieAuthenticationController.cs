using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureWebApplication.Models;
using SecureWebApplication.Services;

namespace SecureWebApplication.Controllers
{
    public class CookieAuthenticationController : Controller
    {
        readonly VerySimpleUserService _userService = new();

        [AllowAnonymous]
        [HttpGet("Account/Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost("Account/Login")]
        public IActionResult Login(LoginModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var principal = _userService.TryAuthenticateUser(model.Username, model.Password);
                if (principal != null)
                {
                    var authProperties = new AuthenticationProperties { RedirectUri = returnUrl };
                    return SignIn(principal, authProperties, CookieAuthenticationDefaults.AuthenticationScheme);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpGet("Account/Logout")]
        public IActionResult Logout()
        {
            var authenticationProperties = new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") };
            return SignOut(authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("Account/AccessDenied")]
        public IActionResult AccessDenied(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("AccessDenied");
        }
    }
}