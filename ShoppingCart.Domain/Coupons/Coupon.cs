namespace ShoppingCart.Domain.Coupons
{
    public class Coupon : DomainBase
    {
        public double MinimumPurchaseAmount { get; set; }
        public double Percentage { get; set; }
    }
}