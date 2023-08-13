using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Models
{
    public class BaseModels
    {
        [Key]
        public int Id { get; set; } 
        public bool Active { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }= DateTime.Now;
        public string? UpdatedBy { get; set; }

    }
}
