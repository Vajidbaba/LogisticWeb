using Microsoft.AspNetCore.Mvc;
using Common.Data.Models;
using System.Diagnostics;
using Common.Core.Services.Contracts;


namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class DashboardController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public DashboardController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<IActionResult> List()
        {
            List<Employee> listEmployees = await _employeeService.GetEmployees();
            return View(listEmployees);
        }
    }
}
