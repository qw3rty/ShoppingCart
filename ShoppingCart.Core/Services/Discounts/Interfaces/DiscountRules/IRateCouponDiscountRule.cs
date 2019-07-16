namespace ShoppingCart.Core.Services.Discounts.Interfaces.DiscountRules
{
    public interface IRateCouponDiscountRule : ICouponDiscountRule
    {
        double MinPurchaseAmount{ get; set; }
        double Percentage{ get; set; }
    }
}