using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductCatalogManager.Bus;
using ProductCatalogManager.Controllers;
using System.Net;
using System.Web.Mvc;

namespace ProductCatalogManager.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            ProductsController controller = new ProductsController(new ProductService());

            ViewResult result = controller.Index("") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details_NullId_ReturnsBadRequest()
        {
            ProductsController controller = new ProductsController(new ProductService());

            var result = controller.Details(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (result as HttpStatusCodeResult).StatusCode);
        }

        [TestMethod]
        public void Details_UnexistingId_ReturnsHttpNotFound()
        {
            ProductsController controller = new ProductsController(new ProductService());

            var result = controller.Details(1);

            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [TestMethod]
        public void Create()
        {
            ProductsController controller = new ProductsController(new ProductService());

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
