﻿using Common.Core.Services;
using Common.Core.Services.Contracts;
using Common.Data.Models;
using Microsoft.AspNetCore.Mvc;


namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class DashboardController : BaseController
    {
        private readonly IUsersService _usersService;
        private readonly IEmployeeService _employeeService;

        public DashboardController(IUsersService usersService, IEmployeeService employeeService )
        {
            _usersService = usersService;
            _employeeService = employeeService;

        }

        public async Task<IActionResult> List(int? Id)
        {
            List<EmployeeModel> data = await _employeeService.GetEmployees();
            return View(data);
        }

        [HttpGet]
        public IActionResult Add(int? id)
        {
            var data = _employeeService.GetEmployeeDetails(id);

            return View(data);
        }


        [HttpPost]
        public IActionResult Add(int? id, EmployeeModel user)
        {
            if (user.Id == 0)
            {
                if (user.Name != null && user.Mobile != null)
                {
                    var getLastId = _employeeService.GetUserCount();
                    int nextId = getLastId + 1;

                    EmployeeModel data = new EmployeeModel
                    {
                        isActive = true,
                        UserId = "EMP" + nextId.ToString("D4"),
                        Name = user.Name,
                        Gender = user.Gender,
                        Mobile = user.Mobile,
                        Email = user.Email,
                        State = user.State,
                        City = user.City,
                        PinCode = user.PinCode,
                        Address = user.Address,
                    };
                    _employeeService.CreateEmployee(data);
                    Toast("User created successfully!", ToastType.SUCCESS);
                    return RedirectToAction("List", "Employee");
                }
            }
            else
            {
                _employeeService.UpdateEmployee(user.Id, user);
                Toast("Updated Successfully!", ToastType.SUCCESS);
                return RedirectToAction("List", "Employee");

            }

            Toast("Please enter all fields", ToastType.ERROR);
            return RedirectToAction("List", "Employee");
        }
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = _employeeService.GetEmployeeDetails(Id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
