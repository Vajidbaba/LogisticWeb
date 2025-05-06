using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Services
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAttendanceByEmployeeIdAsync(int employeeId);
        Task<Attendance> AddAttendanceAsync(Attendance attendance);
        Task<Attendance> UpdateAttendanceAsync(int id, Attendance attendance);
        Task<Attendance> GetAttendanceByIdAsync(int id);
        Task<Attendance> UpdateAttendanceAsync(Attendance attendance);
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IGenericRepository<Users> _repository;
        private readonly LogisticContext _context;
        private readonly IContextHelper _contextHelper;

        public AttendanceService(IGenericRepository<Users> repository, LogisticContext dbcontext, IContextHelper contextHelper)
        {
            _repository = repository;
            _context = dbcontext;
            _contextHelper = contextHelper;
        }

        public async Task<List<Attendance>> GetAttendanceByEmployeeIdAsync(int employeeId)
        {
            // Fetch attendance records by employee ID
            //return await _context.Attendances
            //                     .Where(a => a.EmployeeId == employeeId && a.isActive == true)
            //                     .ToListAsync();
            return await _context.Attendance
                        .Include(a => a.Employee)  // Ensure Employee is included
                        .Where(a => a.EmployeeId == employeeId && a.isActive == true)
                        .ToListAsync();
        }

        public async Task<Attendance> AddAttendanceAsync(Attendance attendance)
        {
            attendance.AddedOn = DateTime.UtcNow;
            attendance.AddedBy = "Admin"; // Replace with the actual user
            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<Attendance> UpdateAttendanceAsync(int id, Attendance attendance)
        {
            // Fetch existing attendance record for updating
            var existingAttendance = await _context.Attendance
                                                     .Where(a => a.Id == id && a.isActive == true)
                                                     .FirstOrDefaultAsync();

            if (existingAttendance == null)
                return null; // If record not found, return null

            // Update the attendance record
            existingAttendance.Status = attendance.Status;
            existingAttendance.Date = attendance.Date;
            existingAttendance.CheckInTime = attendance.CheckInTime;
            existingAttendance.CheckOutTime = attendance.CheckOutTime;
            existingAttendance.UpdatedOn = DateTime.UtcNow;
            existingAttendance.UpdatedBy = "Admin";  // Replace with actual user

            // Save the changes to the database
            await _context.SaveChangesAsync();
            return existingAttendance;
        }
        public async Task<Attendance> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _context.Attendance.Where(a => a.Id == id && a.isActive == true)
                                           .FirstOrDefaultAsync();
            return attendance;  // This will return null if no record is found
        }

        public async Task<Attendance> UpdateAttendanceAsync(Attendance attendance)
        {
            _context.Attendance.Update(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }
       

    }
}
