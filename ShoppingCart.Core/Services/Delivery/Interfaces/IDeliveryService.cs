using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Interfaces;

namespace ShoppingCart.Core.Services.Delivery.Interfaces
{
    public interface IDeliveryService: IService
    {
        double Calculate(CartDto cart, double costPerDelivery, double costPerProduct, double fixedCost);
    }
}