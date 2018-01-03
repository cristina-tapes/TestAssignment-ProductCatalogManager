using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductCatalogManager.Bus.Helpers;
using System;

namespace ProductCatalogManager.Bus.Tests.Helpers
{
    [TestClass]
    public class StringExtensionMethodsTests
    {
        [TestMethod]
        public void Contains_PassContainingStringIgnoreCase_ReturnsTrue()
        {
            string source = "TestString";
            string toFind = "string";

            var result = source.Contains(toFind, StringComparison.OrdinalIgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Contains_PassContainingStringCaseSensitive_ReturnsFalse()
        {
            string source = "TestString";
            string toFind = "string";

            var result = source.Contains(toFind, StringComparison.Ordinal);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Contains_PassEmptyString_ReturnsTrue()
        {
            string source = "TestString";
            string toFind = "";

            var result = source.Contains(toFind, StringComparison.Ordinal);

            Assert.IsTrue(result);
        }
    }
}
