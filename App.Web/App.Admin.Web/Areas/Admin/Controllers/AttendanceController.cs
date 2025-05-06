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
        private readonly LogisticContext _dbcontext;


        public AttendanceController(IAttendanceService attendanceService, LogisticContext dbcontext)
        {
            _attendanceService = attendanceService;
            _dbcontext = dbcontext;

        }

        public async Task<IActionResult> List()
        {
            var records = await _attendanceService.GetAllAttendanceRecords();
            return View(records);
        }

        [HttpGet]
        public IActionResult AddOrUpdate(int? Id)
        {
            AttendanceModel model = Id.HasValue? _attendanceService.GetAttendanceById(Id.Value).Result
                : new AttendanceModel();

            // Populate employees for the dropdown
            ViewBag.EmployeeList = _dbcontext.Employees
       .Select(e => new EmployeeModel { Id = e.Id, Name = e.Name })
       .ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(AttendanceModel attendance)
        {
            if (attendance.Id > 0)
            {
                var updated = await _attendanceService.UpdateAttendance(attendance.Id, attendance);
                Toast(updated ? "Attendance Updated Successfully!" : "Error updating attendance!", updated ? ToastType.SUCCESS : ToastType.ERROR);
            }
            else
            {
                await _attendanceService.AddAttendance(attendance);
                Toast("Attendance Created Successfully!", ToastType.SUCCESS);
            }
            return RedirectToAction("List", "Attendance");
        }
    }
}
