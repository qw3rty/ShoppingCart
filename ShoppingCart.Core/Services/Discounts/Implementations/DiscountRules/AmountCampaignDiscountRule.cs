using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Discounts.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces.DiscountRules;

namespace ShoppingCart.Core.Services.Discounts.Implementations.DiscountRules
{
    public class AmountCampaignDiscountRule : IAmountCampaignDiscountRule
    {
        public AmountCampaignDiscountRule(double price, int quantity)
        {
            Price = price;
            Quantity = quantity;
        }

        public int Quantity { get; set; }
        public ICampaignDiscountRule GetDiscountRule(IList<ProductDto> productList)
        {
            var matchedProductCount = productList.Select(x =>
                x.CategoryList.Select(y => y.CampaignList
                    .Where(z => z.CampaignDiscountRule is IAmountCampaignDiscountRule)
                    .Where(z => z.CampaignDiscountRule == this))).Count();
            if (matchedProductCount >= Quantity)
                return this;
            return null;
        }

        public double CalculateCampaignDiscount(ProductDto productDto, IList<ProductDto> productList)
        {
            if (productList.Count() >= Quantity)
                return Price;
            return 0;
        }

        public double Price { get; set; }
    }
}