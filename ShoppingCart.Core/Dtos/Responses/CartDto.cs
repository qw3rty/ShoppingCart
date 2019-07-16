using System.Collections.Generic;
using ShoppingCart.Core.Services.Discounts.Interfaces;

namespace ShoppingCart.Core.Dtos.Responses
{
    public class CartDto
    {
        public CartDto()
        {
            ProductList = new List<ProductDto>();
        }
        
        public IList<ProductDto> ProductList { get; set; }
        public ICouponDiscountRule Coupon { get; set; }

    }
}