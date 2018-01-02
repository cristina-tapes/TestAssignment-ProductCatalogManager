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
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Login(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Register()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
