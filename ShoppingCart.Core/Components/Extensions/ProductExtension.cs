using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Domain.Products;

namespace ShoppingCart.Core.Components.Extensions
{
    public static class ProductExtension
    {
        
        public static ProductDto ToDto(this Product entity, CategoryDto category)
        {
            if (entity == null)
                return null;

            return new ProductDto
            {
                Title = entity.Title,
                Price = entity.Price,
                Category = category,
            };
        }
        public static ProductDto ToDto(this Product entity)
        {
            if (entity == null)
                return null;

            return new ProductDto
            {
                Title = entity.Title,
                Price = entity.Price,
                Category = entity.Category.ToDto(),
            };
        }
    }
}