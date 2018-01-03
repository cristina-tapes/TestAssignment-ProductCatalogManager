using ProductCatalogManager.Bus.Models;
using System;

namespace ProductCatalogManager.Bus.ModelsDC
{
    public class ProductDC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }

        public ProductDC(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.LastUpdated = product.LastUpdated;
        }
    }
}
