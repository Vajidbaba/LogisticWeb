using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Models
{
    public class Attendance 
    {
        [Key]
        public int Id { get; set; }
        public bool isActive { get; set; }
        public DateTime? AddedOn { get; set; } = DateTime.Now;
        public string? AddedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; } // Ensure Status is not nullable, if status is required.

        // Navigation Property
        [ForeignKey("EmployeeId")]
        public Employees Employee { get; set; }
    }

}
