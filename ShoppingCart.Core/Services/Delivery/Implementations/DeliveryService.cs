using System.Linq;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Implementations;
using ShoppingCart.Core.Services.Delivery.Interfaces;

namespace ShoppingCart.Core.Services.Delivery.Implementations
{
    public class DeliveryService : ServiceBase, IDeliveryService
    {
        public double Calculate(CartDto cart, double costPerDelivery, double costPerProduct, double fixedCost)
        {
            var result = (costPerDelivery * cart.ProductList.GroupBy(x => x.Category.Title).Count()) +
                         (costPerProduct * cart.ProductList.GroupBy(x => x.Title).Count()) +
                         fixedCost;
            return result;
        }
    }
}