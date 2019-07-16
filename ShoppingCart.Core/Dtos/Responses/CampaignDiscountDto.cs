using ShoppingCart.Core.Services.Discounts.Interfaces;

namespace ShoppingCart.Core.Dtos.Responses
{
    public class CampaignDiscountDto
    {
        public double Discount { get; set; }
        public ICampaignDiscountRule Rule { get; set; }
    }
}