using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Systems_ComplexDB.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        [RegularExpression(@"((^[A-Z][a-z]* )[A-Z][a-z]*)", ErrorMessage = "First Name and Last Name should be capitalized")]
        [Display(Name = "SupplierName")]
        public string SupplierName { get; set; }
        public DateTime OrderedAt { get; set; }
        [Required]
        [RegularExpression(@"((^[A-Z][a-z]* )[A-Z][a-z]*)", ErrorMessage = "First Name and Last Name should be capitalized")]
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Price")]
        [Range(1, int.MaxValue, ErrorMessage = "This value can not be 0")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "This value can not be 0")]
        public int Quantity { get; set; }

       
    }
}
