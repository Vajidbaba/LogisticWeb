using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Data.Models
{
    public partial class Employee
    {
        [Key]
        public int IdEmployee { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
