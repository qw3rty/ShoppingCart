using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Discounts.Implementations.DiscountRules;
using ShoppingCart.Core.Services.Discounts.Interfaces;
using ShoppingCart.Domain.Carts;
using ShoppingCart.Domain.Coupons;

namespace ShoppingCart.Core.Components.Extensions
{
    public static class CartExtension
    {
        public static CartDto ToDto(this Cart entity)
        {
            if (entity == null)
                return null;

            return new CartDto
            {
                Coupon = entity.Coupon?.ToDto(),
                ProductList = entity.ProductList?.Select(x => x.ToDto()).ToList() ?? new List<ProductDto>()
            };
        }

        public static ICouponDiscountRule ToDto(this Coupon entity)
        {
            if (entity == null)
                return null;

            return new RateCouponDiscountRule
            (
                entity.Percentage,
                entity.MinimumPurchaseAmount
            );
        }
    }
}