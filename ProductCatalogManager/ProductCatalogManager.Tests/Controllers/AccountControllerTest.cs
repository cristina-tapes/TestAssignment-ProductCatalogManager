using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductCatalogManager.Controllers;
using System.Web.Mvc;

namespace ProductCatalogManager.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Login()
        {
            AccountController controller = new AccountController();

            ViewResult result = controller.Login(null) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Register()
        {
            AccountController controller = new AccountController();

            ViewResult result = controller.Register() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
