using ShoppingCart.Core.Services.Discounts.Interfaces;

namespace ShoppingCart.Core.Dtos.Responses
{
    public class CampaignDto
    {
        public CampaignDto(ICampaignDiscountRule campaignDiscountRule)
        {
            CampaignDiscountRule = campaignDiscountRule;
        }

        public ICampaignDiscountRule CampaignDiscountRule { get; set; }
        
        public class CampaignDiscount
        {
            public double Discount { get; set; }
            public ICampaignDiscountRule Rule { get; set; }
        }
    }
}