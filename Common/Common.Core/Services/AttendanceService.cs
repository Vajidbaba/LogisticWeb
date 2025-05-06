using Common.Data.Context;
using Common.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Services
{
    public interface IAttendanceService
    {
        Task<List<AttendanceModel>> GetAllAttendanceRecords();
        Task<AttendanceModel> GetAttendanceById(int id);
        Task<bool> AddAttendance(AttendanceModel attendance);
        Task<bool> UpdateAttendance(int id, AttendanceModel attendance);
    }
    public class AttendanceService : IAttendanceService
    {
        private readonly LogisticContext _dbcontext;
        private readonly IContextHelper _contextHelper;

        public AttendanceService(LogisticContext dbcontext, IContextHelper contextHelper)
        {
            _dbcontext = dbcontext;
            _contextHelper = contextHelper;
        }

        public async Task<List<AttendanceModel>> GetAllAttendanceRecords()
        {
            return await _dbcontext.Attendance.Where(x => x.Active).ToListAsync();
        }

        public async Task<AttendanceModel> GetAttendanceById(int id)
        {
            return await _dbcontext.Attendance.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> AddAttendance(AttendanceModel attendance)
        {
            var userId = _contextHelper.GetUsername();

            // Validate Status
            if (!Enum.IsDefined(typeof(AttendanceStatus), attendance.Status))
                throw new ArgumentException("Invalid Attendance Status!");

            var newRecord = new AttendanceModel
            {
                EmployeeId = attendance.EmployeeId,
                Date = attendance.Date,
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                WorkHours = attendance.WorkHours,
                Status = attendance.Status,
                Active = true,
                AddedBy = userId,
                AddedOn = DateTime.Now,
                UpdatedBy = userId,
                UpdatedOn = DateTime.Now
            };

            _dbcontext.Attendance.Add(newRecord);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAttendance(int id, AttendanceModel attendance)
        {
            var record = await _dbcontext.Attendance.FirstOrDefaultAsync(x => x.Id == id);
            if (record == null) return false;

            var userId = _contextHelper.GetUsername();

            // Validate Status
            if (!Enum.IsDefined(typeof(AttendanceStatus), attendance.Status))
                throw new ArgumentException("Invalid Attendance Status!");

            record.CheckInTime = attendance.CheckInTime;
            record.CheckOutTime = attendance.CheckOutTime;
            record.WorkHours = attendance.WorkHours;
            record.Status = attendance.Status;
            record.UpdatedBy = userId;
            record.UpdatedOn = DateTime.Now;

            _dbcontext.Attendance.Update(record);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
