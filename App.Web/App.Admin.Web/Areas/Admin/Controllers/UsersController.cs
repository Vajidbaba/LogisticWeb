using Common.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Common.Data.Models;
using System.Diagnostics;
using Common.Core.ViewModels;
using Common.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UsersController : Controller
    {
        private readonly IUsersService _userService;

        private readonly LogisticContext _dbcontext;



        public UsersController(IUsersService userService, LogisticContext dbcontext)
        {
            _userService = userService;
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> List()
        {
            List<Users> listEmployees = await _userService.GetAllUsers();
            return View(listEmployees);
        }
        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Users user)
        {
            if (ModelState.IsValid) {
                var data = new Users();
                data.Active = true;
                data.FullName = user.FullName;
                data.Email = user.Email;
                data.Password = user.Password;
                data.Role = user.Role;

                _userService.CreateNewUser(data);
                return RedirectToAction("List", "Users");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Update(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = _userService.GetUsersDetails(Id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(Users user)
        {
            if (user != null)
            {
                var data = new Users();
                data.Id = user.Id;
                data.Active = user.Active;
                data.FullName = user.FullName;
                data.Email = user.Email;
                data.Password = user.Password;
                data.Role = user.Role;
                _userService.UpdatePerson(data);
                return RedirectToAction("List", "Users");
            }
            return View(user);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = _userService.GetUsersDetails(Id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
