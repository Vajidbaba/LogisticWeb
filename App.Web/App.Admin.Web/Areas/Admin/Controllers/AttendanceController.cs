using Common.Core.Services;
using Common.Data.Context;
using Common.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Admin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IEmployeeServices _employeeServices;
        private readonly LogisticContext _dbcontext;

        public AttendanceController(IAttendanceService attendanceService, LogisticContext dbcontext, IEmployeeServices employeeServices)
        {
            _attendanceService = attendanceService;
            _employeeServices = employeeServices;
            _dbcontext = dbcontext;
        }

        // GET: Admin/Attendance/Index
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeServices.GetAllEmployeesAsync();
            return View(employees);
        }

        // POST: Admin/Attendance/AddAttendance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAttendance(Attendance attendance)
        {
            var employee = await _employeeServices.GetEmployeeByIdAsync(attendance.EmployeeId);

            if (employee == null)
            {
                return NotFound();
            }

            await _attendanceService.AddAttendanceAsync(attendance);
            return RedirectToAction("Index");
        }

        // GET: Admin/Attendance/AttendanceSummary
        public async Task<IActionResult> AttendanceSummary(DateTime? date)
        {
            // If no date is provided, use today's date
            var targetDate = date ?? DateTime.Today;

            // Get attendance records for the target date
            var attendances = await _dbcontext.Attendance.Include(a => a.Employee)
                .Where(a => a.Date.Date == targetDate.Date)
                .ToListAsync();

            return View(attendances);
        }

        // GET: Admin/Attendance/GetFullMonthAttendance
        [HttpGet]
        public async Task<IActionResult> GetFullMonthAttendance(int employeeId)
        {
            var targetMonth = DateTime.Today.Month;
            var targetYear = DateTime.Today.Year;

            var startDate = new DateTime(targetYear, targetMonth, 1); // Start of the month
            var endDate = startDate.AddMonths(1).AddDays(-1); // End of the month

            var attendances = await _dbcontext.Attendance
                .Where(a => a.EmployeeId == employeeId && a.Date >= startDate && a.Date <= endDate)
                .Select(a => new
                {
                    date = a.Date.ToString("dd-MMM-yyyy"),
                    status = a.Status
                })
                .ToListAsync();

            return Json(attendances); // Return the data as JSON to populate the modal
        }

        // POST: Admin/Attendance/UpdateOutTime
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOutTime(int id, DateTime checkOutTime)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
                return NotFound();

            attendance.CheckOutTime = checkOutTime;
            attendance.UpdatedOn = DateTime.UtcNow;
            attendance.UpdatedBy = "Admin"; // Replace with actual user

            await _attendanceService.UpdateAttendanceAsync(attendance);
            return RedirectToAction("AttendanceSummary");
        }

        // POST: Admin/Attendance/UpdateAttendance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAttendance(int id, DateTime? checkInTime, DateTime? checkOutTime, string status)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            attendance.CheckInTime = checkInTime;
            attendance.CheckOutTime = checkOutTime;
            attendance.Status = status;
            attendance.UpdatedOn = DateTime.UtcNow;
            attendance.UpdatedBy = "Admin"; // Replace with actual user

            await _attendanceService.UpdateAttendanceAsync(attendance);

            return RedirectToAction("AttendanceSummary");
        }
    }
}
