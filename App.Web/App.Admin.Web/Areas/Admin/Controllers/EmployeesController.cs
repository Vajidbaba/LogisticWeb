using Common.Core.Services;
using Common.Data.Models;
using Microsoft.AspNetCore.Mvc;
namespace App.Admin.Web.Areas.Admin.Controllers;

[Area("Admin")]

public class EmployeesController : BaseController
{
    private readonly IEmployeeServices _employeeService;

    public EmployeesController(IEmployeeServices employeeService)
    {
        _employeeService = employeeService;
    }

    // GET: Employee
    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return View(employees);
    }

    // GET: Employee/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    // GET: Employee/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,Email,Phone,Department,Designation,JoiningDate,BasicSalary")] Employees employee)
    {
        if (ModelState.IsValid)
        {
            await _employeeService.CreateEmployeeAsync(employee);
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    // POST: Employee/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Phone,Department,Designation,JoiningDate,BasicSalary")] Employees employee)
    {
        if (id != employee.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _employeeService.UpdateEmployeeAsync(id, employee);
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
