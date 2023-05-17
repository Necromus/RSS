using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WpfApp2
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext() { }
        
        public  DbSet<Product> Products { get; set; }

        public DbSet<Stat> Status { get; set; }

        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Storage;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    public class Product
    {
        [Key]
        public int id { get; set; }
        public string? productName { get; set; }
        public string? companyName { get; set; }
    }

    public class Order
    {
        [Key]
        public int id { get; set; }
        public int productId { get; set; }
        public int productCount { get; set; }  
        public string? orderStatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? orderDataFrom { get; set; }
        [Column(TypeName = "date")]
        public DateTime? orderDataTo { get; set; }
    }

    public class Stat
    {
        [Key]
        public string? stat { get; set; }
    }
}
