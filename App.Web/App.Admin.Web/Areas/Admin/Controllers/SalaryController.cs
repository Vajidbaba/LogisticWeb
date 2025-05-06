using Common.Core.Services;
using Microsoft.AspNetCore.Mvc;
namespace App.Admin.Web.Areas.Admin.Controllers;

[Area("Admin")]

public class SalaryController : BaseController
{
    private readonly ISalaryService _salaryService;
    private readonly IEmployeeServices _employeeService;
    private readonly IAttendanceService _attendanceService;

    public SalaryController(ISalaryService salaryService, IEmployeeServices employeeService, IAttendanceService attendanceService)
    {
        _salaryService = salaryService;
        _employeeService = employeeService;
        _attendanceService = attendanceService;
    }

    // Index page showing the list of employees and basic details
    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return View(employees);
    }

    // Generate salary for a specific employee for the given month
    public async Task<IActionResult> GenerateSalary(int employeeId, DateTime month)
    {
        try
        {
            // Call the service to generate the salary
            var salary = await _salaryService.GenerateSalaryAsync(employeeId, month);
            TempData["SuccessMessage"] = "Salary generated successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // View salary details for an employee
    public async Task<IActionResult> ViewSalary(int employeeId)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
        var currentMonthSalary = await _salaryService.GenerateSalaryAsync(employeeId, DateTime.Now);
        var allSalaries = await _salaryService.GetAllSalariesByEmployeeId(employeeId);

        ViewBag.CurrentMonthSalary = currentMonthSalary;
        ViewBag.AllSalaries = allSalaries;
        return View(employee);
    }

    // View attendance details for an employee
    public async Task<IActionResult> AttendanceDetail(int employeeId, string month)
    {
        var attendance = await _salaryService.GetAttendanceByEmployeeIdAndMonthAsync(employeeId, DateTime.Parse(month));
        ViewBag.EmployeeName = (await _employeeService.GetEmployeeByIdAsync(employeeId))?.FullName;
        ViewBag.Month = month;
        return View(attendance);
    }
}
