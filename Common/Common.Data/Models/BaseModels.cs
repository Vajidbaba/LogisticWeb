using System.ComponentModel.DataAnnotations;

namespace Common.Data.Models
{
    public class BaseModels
    {
        [Key]
        public int Id { get; set; } 
        public bool isActive { get; set; }
        public DateTime? AddedOn { get; set; } = DateTime.Now;
        public string? AddedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }= DateTime.Now;
        public string? UpdatedBy { get; set; }
    }

}
