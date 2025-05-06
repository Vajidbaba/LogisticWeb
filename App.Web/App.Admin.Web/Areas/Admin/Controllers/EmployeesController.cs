﻿using Common.Core.Services;
using Common.Core.ViewModels;
using Common.Data.Context;
using Common.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IContextHelper _contextHelper;
        private readonly LogisticContext _dbcontext;

        public EmployeesController(IEmployeeService employeeService, LogisticContext dbcontext, IContextHelper contextHelper)
        {
            _employeeService = employeeService;
            _dbcontext = dbcontext;
            _contextHelper = contextHelper;
        }

        public async Task<IActionResult> List()
        {
            var employees = await _employeeService.GetAllEmployees();

            if (employees == null || employees.Count == 0)
            {
                ViewBag.Message = "No employees found.";
                return View(new List<EmployeeModel>()); // Return empty list instead of null
            }

            return View(employees);
        }


        [HttpGet]
        public IActionResult AddOrUpdate(int? Id)
        {
            EmployeeModel model = Id.HasValue ? _employeeService.GetEmployeeDetails(Id) : new EmployeeModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(EmployeeModel employee)
        {
            if (employee != null)
            {
                if (employee.Id > 0) // Update existing employee
                {
                    var updated = _employeeService.UpdateEmployee(employee.Id, employee);
                    Toast(updated ? "Employee Updated Successfully!" : "Error updating employee!", updated ? ToastType.SUCCESS : ToastType.ERROR);
                }
                else // Add new employee
                {
                    int nextId = _employeeService.GetEmployeeCount() + 1;
                    employee.Id = nextId;
                    _employeeService.CreateEmployee(employee);
                    Toast("Employee Created Successfully!", ToastType.SUCCESS);
                }

                return RedirectToAction("List", "Employees");
            }

            Toast("Please enter all required fields", ToastType.ERROR);
            return View(employee);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null) return NotFound();
            var employee = _employeeService.GetEmployeeDetails(Id);
            return employee == null ? NotFound() : View(employee);
        }
    }
}
