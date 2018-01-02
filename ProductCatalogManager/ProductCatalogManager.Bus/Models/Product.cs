using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ProductCatalogManager.Bus.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        [Range(0.0, double.MaxValue)]
        public double Price { get; set; }
        [Display(Name = "Last Update Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
    }

    public class ProductDBContext : DbContext
    {
        public ProductDBContext() : base("DefaultConnection")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}