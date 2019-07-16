using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces;

namespace ShoppingCart.Core.Services.Coupons.Interfaces
{
    public interface ICouponService : IService
    {
        void AddCoupon(CartDto cart, ICouponDiscountRule coupon);
    }
}