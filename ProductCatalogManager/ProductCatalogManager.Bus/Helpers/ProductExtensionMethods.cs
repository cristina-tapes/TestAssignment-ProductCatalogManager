using ProductCatalogManager.Bus.Models;
using ProductCatalogManager.Bus.ModelsDC;
using System;
using System.Linq;

namespace ProductCatalogManager.Bus.Helpers
{
    public static class ProductExtensionMethods
    {
        public static bool IsEqualTo(this Product thisProduct, Product product)
        {
            if (product == null)
                return false;

            if (object.ReferenceEquals(thisProduct, product))
                return true;

            if (thisProduct.GetType() != product.GetType())
                return false;

            if (thisProduct.Id != product.Id)
                return false;

            if (thisProduct.Name != product.Name)
                return false;

            if (Math.Abs(thisProduct.Price - product.Price) > 0)
                return false;

            if (!thisProduct.Photo.SequenceEqual(product.Photo))
                return false;

            if (!System.DateTime.Equals(thisProduct.LastUpdated, product.LastUpdated))
                return false;

            return true;
        }

        public static bool NotEquals(this Product thisProduct, Product product)
        {
            return !thisProduct.IsEqualTo(product);
        }

        public static ProductDC ToProductDC(this Product product)
        {
            return new ProductDC()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                LastUpdated = product.LastUpdated
            };
        }
    }
}