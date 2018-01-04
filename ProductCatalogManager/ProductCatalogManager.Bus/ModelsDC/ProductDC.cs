using System;

namespace ProductCatalogManager.Bus.ModelsDC
{
    public class ProductDC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
