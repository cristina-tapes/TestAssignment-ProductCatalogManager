using ProductCatalogManager.Bus;
using ProductCatalogManager.Bus.Models;
using ProductCatalogManager.Bus.ModelsDC;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductCatalogManager.Controllers.Api
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        /// <summary>
        /// Get the list of all products without photos
        /// </summary>
        /// <returns>list of products without photos</returns>
        public async Task<IEnumerable<ProductDC>> Get()
        {
            return await _productService.GetProductsDCAsync();
        }

        // GET: api/Products/5
        /// <summary>
        /// Get product by id 
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>product</returns>
        public async Task<Product> Get(int id)
        {
            return await _productService.GetProductAsync(id);
        }
    }
}
