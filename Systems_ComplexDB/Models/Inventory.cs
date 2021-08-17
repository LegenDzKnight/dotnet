using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Systems_ComplexDB.Models
{
    
    public class Inventory
    {
        [Required]
        [Display(Name = "Stock")]
        [Range(1, int.MaxValue, ErrorMessage = "This value can not be 0")]
        public int Stock { get; set; }
        public DateTime LastUpdated { get; set; }
        [Key]
        [ForeignKey("Product")]
        public int ProductRefID { get; set; }
        public Product Product { get; set; }
    }
}
