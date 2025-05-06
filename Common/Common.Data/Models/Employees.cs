using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Models
{
    public class Employees : BaseModels
    {
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Department { get; set; }

        public string? Designation { get; set; }
        public DateTime? JoiningDate { get; set; }
        public decimal? BasicSalary { get; set; }

        // Navigation
        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<OvertimeRecord>? OvertimeRecords { get; set; }
        public ICollection<Salary>? Salaries { get; set; }
    }

}
