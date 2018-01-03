using ProductCatalogManager.Bus;
using System.Collections.Generic;
using System.Web.Http;

namespace ProductCatalogManager.Controllers.Api
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private ProductBus productBus = new ProductBus();

        // GET: api/Products
        /// <summary>
        /// Get the list of all products without photos
        /// </summary>
        /// <returns>list of products without photos</returns>
        public IEnumerable<object> Get()
        {
            return productBus.GetProductsDC();
        }

        // GET: api/Products/5
        /// <summary>
        /// Get product by id 
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>product</returns>
        public object Get(int id)
        {
            return productBus.GetProduct(id);
        }
    }
}
