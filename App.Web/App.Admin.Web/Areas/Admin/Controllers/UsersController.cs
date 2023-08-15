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

    public class UsersController : BaseController
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
        public IActionResult Create(UsersModel user)
        {
            UsersModel data = new UsersModel();
            if (data != null)
            {
                data.Active = true;
                data.Username = user.Username;
                data.Mobile = user.Mobile;
                data.Password = user.Password;
                data.Role = user.Role;
                _userService.CreateUser(data);

                Toast("User created successfully!", ToastType.SUCCESS);
                return RedirectToAction("List", "Users");
            }
            Toast("Please enter all fields", ToastType.ERROR);
            return RedirectToAction("List", "Users");
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
        public IActionResult UpdateUser(int id, UsersModel user)
        {
            if (user != null)
            {
                var data = new UsersModel();
                data.Id = user.Id;
                data.Active = user.Active;
                data.Username = user.Username;
                data.Mobile  = user.Mobile;
                data.Password = user.Password;
                data.Role = user.Role;
                _userService.UpdateUser(id, data);
                Toast("User Updated Successfully!", ToastType.SUCCESS);
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
