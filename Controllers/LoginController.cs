using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yorokoanime.DataAccessLayer;
using yorokoanime.Models;
using yorokoanime.ViewModels;

namespace yorokoanime.Controllers;

public class LoginController : Controller
{
    private readonly DatabaseMethods _dbMethods;

    public LoginController(DatabaseMethods dbMethods)
    {
        _dbMethods = dbMethods;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? message = null)
    {
        if(message == null)
        {
            ViewData["message"] = message;
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserAccount model, string? returnURL = null)
    {
        UserAccount user = _dbMethods.GetUser(model.Username);
        if (user == null)
        {
            ViewBag.ErrorMessage = "Account not found, please try again or register a new account";
            return View(model);
        }
        
        if(model?.Password != null || model?.Password != "")
        {
            if (Helpers.PasswordHasher.VerifyPassword(model.Password, user.Password))
            {
                // Create your own authentication ticket and sign the user in using cookies.
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("IsAdmin", user.IsAdmin.ToString()), 
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, Settings.AuthCookiName);
                var principal = new ClaimsPrincipal(claimsIdentity);

                var props = new AuthenticationProperties
                {
                    IsPersistent = true, //Can add remember checkbox to control this
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                };
                
                await HttpContext.SignInAsync(Settings.AuthCookiName, principal, props);
            
                return RedirectToAction("Index", "Home");
            }
        }

        ViewBag.ErrorMessage = "Invalid username or password";
        model.Password = string.Empty;
        return View(model);
    }
    
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserRegister model)
    {
        if (ModelState.IsValid)
        {
            if (_dbMethods.RegisterUser(model))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.ErrorMessage = "Username or Email already exists. Please choose a different one.";
                return View(model);
            }
        }
        else
        {
            return View(model);
        }

        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(Settings.AuthCookiName);
        return RedirectToAction("Index", "Home");
    }
}