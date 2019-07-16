using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Implementations;
using ShoppingCart.Core.Services.Campaigns.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces;

namespace ShoppingCart.Core.Services.Campaigns.Implementations
{
    public class CampaignService : ServiceBase, ICampaignService
    {
        public CampaignDto Get(ICampaignDiscountRule campaignDiscountRule)
        {
           return new CampaignDto(campaignDiscountRule);
        }
    }
}