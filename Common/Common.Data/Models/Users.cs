using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Data.Models
{
    public class Users: BaseModels
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }


    }
}
