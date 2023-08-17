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
        private readonly IContextHelper _contextHelper;

        private readonly LogisticContext _dbcontext;

        public UsersController(IUsersService userService, LogisticContext dbcontext, IContextHelper contextHelper)
        {
            _userService = userService;
            _dbcontext = dbcontext;
            _contextHelper = contextHelper;
        }
        public async Task<IActionResult> List()
        {
            var userName = _contextHelper.GetUsername();
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
                if (user.Username != null)
                {
                    var getLastId = _userService.GetLastUsersId();
                    string inputString = user.Username;
                    char firstLetter = inputString[0];
                    string inputStrings = user.Username;
                    string[] words = inputStrings.Split(' ');
                    string lastWord = words[words.Length - 1];

                    data.Active = true;
                    data.UserId = firstLetter.ToString().ToLower() + lastWord.ToLower() + getLastId;
                    data.Username = user.Username;
                    data.Mobile = user.Mobile;
                    data.Password = user.Password;
                    data.Role = user.Role;
                    _userService.CreateUser(data);
                }
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
                data.Mobile = user.Mobile;
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
