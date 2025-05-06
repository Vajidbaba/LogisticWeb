using Common.Data.Context;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Services
{
    public interface ISalaryService
    {
        Task<List<Salary>> GetSalaryByEmployeeIdAsync(int employeeId);
        Task<Salary> GenerateSalaryAsync(Salary salary);
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

            public async Task<List<Salary>> GetSalaryByEmployeeIdAsync(int employeeId)
            {
                return await _context.Salaries
                                     .Where(s => s.EmployeeId == employeeId && s.isActive == true)
                                     .ToListAsync();
            }

            public async Task<Salary> GenerateSalaryAsync(Salary salary)
            {
                salary.AddedOn = DateTime.UtcNow;
                salary.AddedBy = "Admin"; // Replace with actual user
                salary.TotalSalary = salary.BasicSalary + salary.OvertimeAmount;
                _context.Salaries.Add(salary);
                await _context.SaveChangesAsync();
                return salary;
            }
        }

    }
}
