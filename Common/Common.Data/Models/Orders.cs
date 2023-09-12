using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Models
{
    public  class Orders: BaseModels
    {
        public string? OrderID { get; set; }
        public string? CustomerID { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Ship Date")]
        [DataType(DataType.Date)]
        public DateTime ShipDate { get; set; }

        [Display(Name = "Total Amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Shipping Address")]
        [StringLength(255)]
        public string? ShippingAddress { get; set; }

        [Display(Name = "Payment Method")]
        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [Display(Name = "Order Status")]
        [StringLength(20)]
        public string? OrderStatus { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        [Display(Name = "Mobile Number")]
        public string? Mobile { get; set; }
    }

}
