using ShoppingCart.Core.Services.Discounts.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces.DiscountRules;

namespace ShoppingCart.Core.Services.Discounts.Implementations.DiscountRules
{
    public class RateCouponDiscountRule : IRateCouponDiscountRule
    {
        public RateCouponDiscountRule(double percentage, double minPurchaseAmount)
        {
            Percentage = percentage;
            MinPurchaseAmount = minPurchaseAmount;
        }


        public double MinPurchaseAmount { get; set; }
        public double Percentage { get; set; }
    }
}