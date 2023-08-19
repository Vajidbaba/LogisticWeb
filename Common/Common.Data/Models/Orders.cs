using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Models
{
    public  class Orders
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }


    }

}
