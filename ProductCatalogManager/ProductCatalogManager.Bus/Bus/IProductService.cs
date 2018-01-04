using ProductCatalogManager.Bus.Models;
using ProductCatalogManager.Bus.ModelsDC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalogManager.Bus
{
    public interface IProductService
    {
        List<Product> GetProducts(string filter = "");

        Task<List<ProductDC>> GetProductsDCAsync();

        Product GetProduct(int id);

        Task<Product> GetProductAsync(int id);

        void Add(Product product);

        void Update(Product product);

        void Delete(int id);

        byte[] ExportAsExcel();
    }
}
