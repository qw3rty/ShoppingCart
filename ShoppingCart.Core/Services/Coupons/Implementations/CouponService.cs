using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Implementations;
using ShoppingCart.Core.Services.Coupons.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces;

namespace ShoppingCart.Core.Services.Coupons.Implementations
{
    public class CouponService : ServiceBase, ICouponService
    {
        public void AddCoupon(CartDto cart, ICouponDiscountRule coupon)
        {
            cart.Coupon = coupon;
        }
    }
}