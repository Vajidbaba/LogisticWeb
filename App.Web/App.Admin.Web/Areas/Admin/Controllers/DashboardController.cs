using Microsoft.AspNetCore.Mvc;
using Common.Data.Models;
using System.Diagnostics;
using Common.Core.Services.Contracts;


namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class DashboardController : Controller
    {
        private readonly IUsersService _usersService;
        public DashboardController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public async Task<IActionResult> List()
        {
            var users = await _usersService.GetAllUsers();  
            return View(users);
        }
    }
}
