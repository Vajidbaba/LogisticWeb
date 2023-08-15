using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Data.Models
{
    public class Users: BaseModels
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        public string? Mobile { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }


    }
}
