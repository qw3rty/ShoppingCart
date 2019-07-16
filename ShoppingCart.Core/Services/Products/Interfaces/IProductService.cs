using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Interfaces;

namespace ShoppingCart.Core.Services.Products.Interfaces
{
    public interface IProductService : IService
    {
        ProductDto Get(string name, double price, CategoryDto category);
    }
}