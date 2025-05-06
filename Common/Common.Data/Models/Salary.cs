using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Models
{
    public class Salary : BaseModels
    {
        public int EmployeeId { get; set; }
        public DateTime Month { get; set; } // e.g., 2024-06-01
        public decimal BasicSalary { get; set; }
        public decimal OvertimeAmount { get; set; }
        public decimal TotalSalary { get; set; }

        // Navigation
        public Employees? Employee { get; set; }
    }

}
