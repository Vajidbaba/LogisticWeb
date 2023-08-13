using Common.Core.Services.Contracts;
using Common.Data.Models;
using Common.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _repository;

        public EmployeeService(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                return await _repository.GetEmployees();
            }
            catch
            {
                throw;
            }
        }
    }
}
