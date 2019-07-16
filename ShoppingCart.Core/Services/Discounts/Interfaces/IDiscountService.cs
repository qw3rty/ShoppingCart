using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Interfaces;

namespace ShoppingCart.Core.Services.Discounts.Interfaces
{
    public interface IDiscountService : IService
    {
        ICampaignDiscountRule GetRateCampaignDiscountRule(double percentage, int quantity);
        ICampaignDiscountRule GetAmountCampaignDiscountRule(double price, int quantity);
        ICouponDiscountRule GetAmountCouponDiscountRule(double percentage, double minPurchaseAmount);
        double CalculateCampaignDiscount(CartDto cart);
        double CalculateCouponDiscount(ICouponDiscountRule cartCoupon, double cartTotal, double campaignDiscount);
    }
}