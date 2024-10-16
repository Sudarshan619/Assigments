using Day26_EntityFramework.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day26_EntityFramework.Context
{
    public class ShoppingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-JIQ20LLJ\\ROOT;Integrated Security=true;Initial Catalog=dbEFCode15Oct24;TrustServerCertificate=True");
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<SupplierProducts> SupplierProducts { get; set; }
    }
}
