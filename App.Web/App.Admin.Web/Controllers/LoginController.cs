using Common.Core.Services.Contracts;
using Common.Core.ViewModels;
using Common.Data.Context;
using Common.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace App.Admin.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly LogisticContext _dbcontext;
        private const string ACCOUNT_UID_PASSWORD = "Username or Password Incorrect";
        private const string ACCOUNT_DESABLE = "Username or Password Incorrect";


        public LoginController(LogisticContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginVM();
#if DEBUG
            model.Email = "test@test.com";
#endif
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                Toast("Data", ToastType.ERROR);
                return View(model);
            }
            var userInfo = new Users();

            userInfo = _dbcontext.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();


            if (userInfo == null || string.IsNullOrEmpty(userInfo.Email))
            {
                Toast("Data", ToastType.ERROR);
                return View(model);
            }
            if (!userInfo.Active)
            {
                Toast("Data", ToastType.ERROR);
                return View(model);
            }
            return CreateIdentity(model.Email, userInfo.Role);


        }

        [NonAction]
        private IActionResult CreateIdentity(string userName, string role)
        {
            var claims = new List<Claim>();
            {
                new Claim(ClaimTypes.Name, userName);
                new Claim(ClaimTypes.Role, role);
            };
            var claimIdentiy = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            };
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentiy), authProperties);
            return Redirect($"~/admin/dashboard/list");
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
