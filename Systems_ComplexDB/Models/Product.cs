using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Systems_ComplexDB.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [RegularExpression(@"((^[A-Z][a-z]* )[A-Z][a-z]*)", ErrorMessage = "First Name and Last Name should be capitalized")]
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Price")]
        [Range(1, int.MaxValue, ErrorMessage = "This value can not be 0")]
        public int Price { get; set; }
    
    }
}
