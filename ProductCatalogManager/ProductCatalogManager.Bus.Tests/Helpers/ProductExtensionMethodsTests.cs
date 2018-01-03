using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductCatalogManager.Bus.Helpers;
using ProductCatalogManager.Bus.Models;
using System;

namespace ProductCatalogManager.Bus.Tests.Helpers
{
    [TestClass]
    public class ProductExtensionMethodsTests
    {
        [TestMethod]
        public void IsEqualTo_PassNull_ReturnsFalse()
        {
            var product = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now };

            var result = product.IsEqualTo(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEqualTo_PassSameObject_ReturnsTrue()
        {
            var product = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now };

            var result = product.IsEqualTo(product);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEqualTo_PassIdenticalObjectDifferentId_ReturnsFalse()
        {
            var product = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now };
            var productToCompare = new Product() { Id = 2, Name = "Product1", LastUpdated = DateTime.Now };

            var result = product.IsEqualTo(productToCompare);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEqualTo_PassIdenticalObjectDifferentName_ReturnsFalse()
        {
            var product = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now };
            var productToCompare = new Product() { Id = 1, Name = "Product2", LastUpdated = DateTime.Now };

            var result = product.IsEqualTo(productToCompare);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEqualTo_PassIdenticalObjectDifferentPrice_ReturnsFalse()
        {
            var product = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now };
            var productToCompare = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now, Price = 1 };

            var result = product.IsEqualTo(productToCompare);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEqualTo_PassIdenticalObjectDifferentPhoto_ReturnsFalse()
        {
            var product = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now, Photo = new byte[] { 1, 1 } };
            var productToCompare = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now, Photo = new byte[] { 0, 0 } };

            var result = product.IsEqualTo(productToCompare);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEqualTo_PassIdenticalObjectDifferentLastUpdateDate_ReturnsFalse()
        {
            var product = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now, Photo = new byte[] { 1, 1 } };
            var productToCompare = new Product() { Id = 1, Name = "Product1", LastUpdated = DateTime.Now.AddDays(-1), Photo = new byte[] { 0, 0 } };

            var result = product.IsEqualTo(productToCompare);

            Assert.IsFalse(result);
        }
    }
}
