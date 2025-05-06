using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Services
{
    public interface ISalaryService
    {
        Task<Salary> GenerateSalaryAsync(int employeeId, DateTime month);
        Task<List<Salary>> GetSalariesAsync();
        Task<Salary> GetSalaryByEmployeeIdAndMonthAsync(int employeeId, DateTime month);
        Task<List<OvertimeRecord>> GetOvertimesByMonthAsync(int employeeId, DateTime month);
        Task<List<Salary>> GetAllSalariesByEmployeeId(int employeeId);
        Task<List<Attendance>> GetAttendanceByEmployeeIdAndMonthAsync(int employeeId, DateTime month);
    }

    public class SalaryService : ISalaryService
    {
        private readonly IGenericRepository<Users> _repository;
        private readonly LogisticContext _context;
        private readonly IContextHelper _contextHelper;

        public SalaryService(IGenericRepository<Users> repository, LogisticContext dbcontext, IContextHelper contextHelper)
        {
            _repository = repository;
            _context = dbcontext;
            _contextHelper = contextHelper;
        }
        public async Task<List<Salary>> GetAllSalariesByEmployeeId(int employeeId)
        {
            return await _context.Salaries
                                 .Where(s => s.EmployeeId == employeeId && s.isActive)
                                 .OrderByDescending(s => s.Month)
                                 .ToListAsync();
        }

        // Generate Salary based on BasicSalary + Overtime
        public async Task<Salary> GenerateSalaryAsync(int employeeId, DateTime month)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null) throw new Exception("Employee not found");

            // Get all attendance records for the month
            var attendance = await _context.Attendance
                .Where(a => a.EmployeeId == employeeId && a.Date.Month == month.Month && a.Date.Year == month.Year && a.isActive)
                .ToListAsync();

            // Exclude Sundays from working days
            int totalWorkingDays = attendance.Count(a => a.Date.DayOfWeek != DayOfWeek.Sunday);
            int presentDays = attendance.Count(a => a.Status == "Present" && a.Date.DayOfWeek != DayOfWeek.Sunday);
            int leaveDays = attendance.Count(a => a.Status == "Leave" && a.Date.DayOfWeek != DayOfWeek.Sunday);
            int absentDays = attendance.Count(a => a.Status == "Absent" && a.Date.DayOfWeek != DayOfWeek.Sunday);

            // If no leave or absent days, full salary is given
            decimal basicSalaryForPresentDays = 0;
            if (leaveDays == 0 && absentDays == 0)
            {
                basicSalaryForPresentDays = employee.BasicSalary ?? 0;
            }
            else
            {
                decimal basicSalaryPerDay = (decimal)(employee.BasicSalary ?? 0) / totalWorkingDays;
                basicSalaryForPresentDays = basicSalaryPerDay * presentDays;
            }

            // Get overtime records for the month
            var overtimes = await _context.OvertimeRecords
                .Where(o => o.EmployeeId == employeeId && o.Date.Month == month.Month && o.Date.Year == month.Year && o.isActive)
                .ToListAsync();

            // Calculate overtime amount
            decimal overtimeAmount = overtimes.Sum(o => (decimal)o.Hours * o.RatePerHour);

            // Create Salary record
            var salary = new Salary
            {
                EmployeeId = employeeId,
                Month = new DateTime(month.Year, month.Month, 1),
                BasicSalary = basicSalaryForPresentDays,
                OvertimeAmount = overtimeAmount,
                TotalSalary = basicSalaryForPresentDays + overtimeAmount,
                AddedOn = DateTime.UtcNow,
                AddedBy = "Admin", // Replace with actual user
                isActive = true
            };

            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();
            return salary;
        }
        public async Task<List<Attendance>> GetAttendanceByEmployeeIdAndMonthAsync(int employeeId, DateTime month)
        {
            var startDate = new DateTime(month.Year, month.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return await _context.Attendance
                .Where(a => a.EmployeeId == employeeId &&
                            a.Date >= startDate &&
                            a.Date <= endDate &&
                            a.isActive)
                .OrderBy(a => a.Date)
                .ToListAsync();
        }

        // Get All Salaries
        public async Task<List<Salary>> GetSalariesAsync()
        {
            return await _context.Salaries
                .Include(s => s.Employee) // Include employee details
                .Where(s => s.isActive)
                .ToListAsync();
        }

        // Get Salary for a specific Employee and Month
        public async Task<Salary> GetSalaryByEmployeeIdAndMonthAsync(int employeeId, DateTime month)
        {
            return await _context.Salaries
                .Where(s => s.EmployeeId == employeeId && s.Month.Year == month.Year && s.Month.Month == month.Month && s.isActive)
                .FirstOrDefaultAsync();
        }

        // Get Overtime records for a specific Employee and Month
        public async Task<List<OvertimeRecord>> GetOvertimesByMonthAsync(int employeeId, DateTime month)
        {
            return await _context.OvertimeRecords
                .Where(o => o.EmployeeId == employeeId && o.Date.Month == month.Month && o.Date.Year == month.Year && o.isActive)
                .ToListAsync();
        }
    }
}
