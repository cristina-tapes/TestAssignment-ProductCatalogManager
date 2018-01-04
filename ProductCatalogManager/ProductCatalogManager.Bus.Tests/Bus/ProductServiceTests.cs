using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductCatalogManager.Bus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProductCatalogManager.Bus.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestMethod]
        public void GetProducts()
        {
            var productsInDB = new List<Product>
            {
                new Product() {Id = 1, Name = "Product1", Photo = new byte[0], Price = 100.0, LastUpdated = DateTime.Now}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productsInDB.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productsInDB.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productsInDB.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(productsInDB.GetEnumerator);

            var mockContext = new Mock<ProductDBContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object);
            var products = service.GetProducts();

            Assert.AreEqual(1, products.Count());
            Assert.AreEqual(productsInDB.ElementAt(0).Name, products.ElementAt(0).Name);
        }

        [TestMethod]
        public void Add()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Product1",
                Photo = new byte[0],
                Price = 100.0,
                LastUpdated = DateTime.Now
            };

            var mockSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<ProductDBContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object);
            service.Add(product);

            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Detele()
        {
            var mockSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<ProductDBContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object);
            service.Delete(1);

            mockSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public async void GetProductsDC()
        {
            var productsInDB = new List<Product>
            {
                new Product() {Id = 1, Name = "Product1", Photo = new byte[0], Price = 100.0, LastUpdated = DateTime.Now}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productsInDB.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productsInDB.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productsInDB.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(productsInDB.GetEnumerator);

            var mockContext = new Mock<ProductDBContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object);
            var productsDC = await service.GetProductsDCAsync();

            Assert.AreEqual(1, productsDC.Count());
            Assert.AreEqual(productsInDB.ElementAt(0).Name, productsDC.ElementAt(0).Name);
        }

        [TestMethod]
        public void GetProduct()
        {
            var productsInDB = new List<Product>
            {
                new Product() {Id = 1, Name = "Product1", Photo = new byte[0], Price = 100.0, LastUpdated = DateTime.Now}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productsInDB.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productsInDB.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productsInDB.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(productsInDB.GetEnumerator);

            var mockContext = new Mock<ProductDBContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object);
            var product = service.GetProduct(1);

            Assert.IsNotNull(product);
            Assert.AreEqual(productsInDB.ElementAt(0).Name, product.Name);
        }
    }
}
