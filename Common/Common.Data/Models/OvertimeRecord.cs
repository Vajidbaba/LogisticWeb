using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Models
{
    public class OvertimeRecord : BaseModels
    {
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public float Hours { get; set; }
        public decimal RatePerHour { get; set; }

        // Navigation
        public Employees? Employee { get; set; }
    }

}
