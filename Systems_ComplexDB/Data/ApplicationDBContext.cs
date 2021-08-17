using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Systems_ComplexDB.Models;

namespace Systems_ComplexDB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }



        public DbSet<Product> Product { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Order> Order { get; set; }

     
    }
}
