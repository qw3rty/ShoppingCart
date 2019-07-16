using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Interfaces;

namespace ShoppingCart.Core.Services.Carts.Interfaces
{
    public interface ICartService : IService
    {
        CartDto Get();
        void AddItem(CartDto cart, ProductDto product, int quantity);
        string Print(CartDto cart);
        double GetTotalAmountAfterDiscounts(CartDto cart);
        double GetCouponDiscount(CartDto cart);
        double GetCampaignDiscount(CartDto cart);
        double GetDeliveryDiscount(CartDto cart);
    }
}