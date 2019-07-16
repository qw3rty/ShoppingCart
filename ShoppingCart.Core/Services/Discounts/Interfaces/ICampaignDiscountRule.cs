using System.Collections.Generic;
using ShoppingCart.Core.Dtos.Responses;

namespace ShoppingCart.Core.Services.Discounts.Interfaces
{
    public interface ICampaignDiscountRule
    {
        int Quantity { get; set; }
        ICampaignDiscountRule GetDiscountRule(IList<ProductDto> productList);
        double CalculateCampaignDiscount(ProductDto productDto,IList<ProductDto> productList);
    }
}