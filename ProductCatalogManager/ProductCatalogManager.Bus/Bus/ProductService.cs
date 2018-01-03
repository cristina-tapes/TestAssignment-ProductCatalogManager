using ProductCatalogManager.Bus.Helpers;
using ProductCatalogManager.Bus.Models;
using ProductCatalogManager.Bus.ModelsDC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProductCatalogManager.Bus
{
    public class ProductService : IProductService, IDisposable
    {
        private ProductDBContext dbContext;

        public ProductService() : this(new ProductDBContext())
        {
        }

        public ProductService(ProductDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Product> GetProducts(string filter = "")
        {
            var products = dbContext.Products;

            if (!string.IsNullOrEmpty(filter))
                return products.Where(p => p.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

            return products.ToList();
        }

        public IEnumerable<ProductDC> GetProductsDC()
        {
            return dbContext.Products.AsEnumerable().Select(p => new ProductDC(p));
        }

        public Product GetProduct(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            Product uneditedProduct = dbContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);

            if (product.Photo.Length == 0)
                product.Photo = uneditedProduct.Photo;

            if (uneditedProduct.NotEquals(product))
            {
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Product product = dbContext.Products.Find(id);
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }

        public byte[] ExportAsExcel()
        {
            var dataToExport = dbContext.Products.AsEnumerable().Select(p => new ProductDC(p));
            return ExcelExporter.ExportDataToExcel(dataToExport.ToList(), typeof(Product).Name);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
