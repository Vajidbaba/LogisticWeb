using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Services
{
    public interface IEmployeeServices
    {
        Task<List<Employees>> GetAllEmployeesAsync();
        Task<Employees> GetEmployeeByIdAsync(int id);
        Task<Employees> CreateEmployeeAsync(Employees employee);
        Task<Employees> UpdateEmployeeAsync(int id, Employees employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IGenericRepository<Users> _repository;
        private readonly LogisticContext _context;
        private readonly IContextHelper _contextHelper;
        public EmployeeServices(IGenericRepository<Users> repository, LogisticContext dbcontext, IContextHelper contextHelper)
        {
            _repository = repository;
            _context = dbcontext;
            _contextHelper = contextHelper;
        }

        public async Task<List<Employees>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Where(e => e.isActive == true).ToListAsync();
        }

        public async Task<List<Attendance>> GetAllAttendanceAsync()
        {
            return await _context.Attendance
                .Include(a => a.Employee) // Include related data if necessary
                .ToListAsync(); // Make sure this fetches all records
        }
        public async Task<Employees> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.isActive == true);
        }

        public async Task<Employees> CreateEmployeeAsync(Employees employee)
        {
            employee.AddedOn = DateTime.UtcNow;
            employee.AddedBy = "Admin"; // Replace with actual user
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employees> UpdateEmployeeAsync(int id, Employees employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null) return null;

            existingEmployee.FullName = employee.FullName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.Department = employee.Department;
            existingEmployee.Designation = employee.Designation;
            existingEmployee.UpdatedOn = DateTime.UtcNow;
            existingEmployee.UpdatedBy = "Admin";

            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            employee.isActive = false;
            employee.UpdatedOn = DateTime.UtcNow;
            employee.UpdatedBy = "Admin";

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
