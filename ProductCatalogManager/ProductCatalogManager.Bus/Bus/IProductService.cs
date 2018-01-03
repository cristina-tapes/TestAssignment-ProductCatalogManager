using ProductCatalogManager.Bus.Models;
using ProductCatalogManager.Bus.ModelsDC;
using System.Collections.Generic;

namespace ProductCatalogManager.Bus
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(string filter = "");

        IEnumerable<ProductDC> GetProductsDC();

        Product GetProduct(int id);

        void Add(Product product);

        void Update(Product product);

        void Delete(int id);

        byte[] ExportAsExcel();
    }
}
