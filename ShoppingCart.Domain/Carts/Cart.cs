using System.Collections.Generic;
using ShoppingCart.Domain.Coupons;
using ShoppingCart.Domain.Products;

namespace ShoppingCart.Domain.Carts    
{
    public class Cart : DomainBase
    {
        public IList<Product> ProductList { get; set; }
        public Coupon Coupon { get; set; }
    }
}