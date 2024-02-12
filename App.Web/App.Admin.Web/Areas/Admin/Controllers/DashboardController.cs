using Microsoft.AspNetCore.Mvc;
using Common.Data.Models;
using System.Diagnostics;
using Common.Core.Services.Contracts;
using Common.Core.Services;

namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class DashboardController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUsersService _userService;
        private readonly IOrdersService _orderService;


        public DashboardController(IEmployeeService employeeService, IUsersService userService, IOrdersService ordersService)
        {
            _employeeService = employeeService;
            _userService = userService;
            _orderService = ordersService;
        }
        public async Task<IActionResult> List()
        {
            var UserCount = _userService.GetUserCount();
            var ProcessingCount = _orderService.GetProcessingCount();
            var ShippedCount = _orderService.GetShippedCount();
            var DeliveredCount = _orderService.GetDeliveredCount();

            ViewBag.UserCount = UserCount;
            ViewBag.ProcessingCount = ProcessingCount;
            ViewBag.ShippedCount = ShippedCount;
            ViewBag.DeliveredCount = DeliveredCount;



            List<Employee> listEmployees = await _employeeService.GetEmployees();
            return View(listEmployees);
        }

    }
}
