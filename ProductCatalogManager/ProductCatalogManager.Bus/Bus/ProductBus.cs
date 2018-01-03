using ProductCatalogManager.Bus.Helpers;
using ProductCatalogManager.Bus.Models;
using ProductCatalogManager.Bus.ModelsDC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProductCatalogManager.Bus
{
    public class ProductBus : IDisposable
    {
        private ProductDBContext db = new ProductDBContext();

        public IEnumerable<Product> GetProducts(string filter = "")
        {
            var products = db.Products;

            if (!string.IsNullOrEmpty(filter))
                return products.Where(p => p.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

            return products.ToList();
        }

        public IEnumerable<ProductDC> GetProductsDC()
        {
            return db.Products.AsEnumerable().Select(p => new ProductDC(p));
        }

        public Product GetProduct(int id)
        {
            return db.Products.Find(id);
        }

        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Update(Product product)
        {
            Product uneditedProduct = db.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);

            if (product.Photo.Length == 0)
                product.Photo = uneditedProduct.Photo;

            if (uneditedProduct.NotEquals(product))
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public byte[] ExportAsExcel()
        {
            var dataToExport = db.Products.AsEnumerable().Select(p => new ProductDC(p));
            return ExcelExporter.ExportDataToExcel(dataToExport.ToList(), typeof(Product).Name);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
