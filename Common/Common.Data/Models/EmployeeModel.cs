using System.ComponentModel.DataAnnotations;

namespace Common.Data.Models
{
    public partial class EmployeeModel: BaseModels
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string? Address { get; set; }
    }
}
