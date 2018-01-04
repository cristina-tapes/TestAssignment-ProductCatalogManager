using ProductCatalogManager.Bus.Helpers;
using ProductCatalogManager.Bus.Models;
using ProductCatalogManager.Bus.ModelsDC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogManager.Bus
{
    public class ProductService : IProductService, IDisposable
    {
        private ProductDBContext _dbContext;

        public ProductService() : this(new ProductDBContext())
        {
        }

        public ProductService(ProductDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetProducts(string filter = "")
        {
            var products = _dbContext.Products.ToList();

            if (!string.IsNullOrEmpty(filter))
                return products
                        .Where(p => p.Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
                        .ToList();

            return products;
        }

        public Task<List<ProductDC>> GetProductsDCAsync()
        {
            return _dbContext.Products
                .Select(p => new ProductDC
                {
                    Id = p.Id,
                    LastUpdated = p.LastUpdated,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToListAsync();
        }

        public Product GetProduct(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public Task<Product> GetProductAsync(int id)
        {
            return _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            var uneditedProduct = _dbContext.Products
                                    .AsNoTracking()
                                    .FirstOrDefault(p => p.Id == product.Id);

            if (product.Photo.Length == 0)
                product.Photo = uneditedProduct.Photo;

            if (uneditedProduct.NotEquals(product))
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var product = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public byte[] ExportAsExcel()
        {
            var dataToExport = _dbContext.Products
                                .AsEnumerable()
                                .Select(p => p.ToProductDC())
                                .ToList();

            return ExcelExporter.ExportDataToExcel(dataToExport, typeof(Product).Name);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
