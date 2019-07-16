using System;
using ShoppingCart.Core.Components.Constants;
using ShoppingCart.Core.Components.Extensions;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Implementations;
using ShoppingCart.Core.Services.Products.Interfaces;
using ShoppingCart.Domain.Products;

namespace ShoppingCart.Core.Services.Products.Implementations
{
    public class ProductService : ServiceBase, IProductService
    {
        public ProductDto Get(string name, double price, CategoryDto category)
        {
            ValidateGet(name, price, category);
            return new Product(name, price).ToDto(category);
        }

        #region Utils

        #region ValidateGet

        private void ValidateGet(string name, double price, CategoryDto category)
        {
            ValidateGetName(name);
            ValidateGetPrice(price);
            ValidateGetCategory(category);
        }

        private void ValidateGetName(string name)
        {
            if (name.Length < 1)
                throw new Exception(ResourceConstantsExceptions.NotValid("Name"));
        }

        private void ValidateGetPrice(double price)
        {
            if (price <= 0)
                throw new Exception(ResourceConstantsExceptions.NotValid("Price"));
        }

        private void ValidateGetCategory(CategoryDto category)
        {
            if (category == null)
                throw new Exception(ResourceConstantsExceptions.NotFound("Category"));
        }

        #endregion

        #endregion

    }
}