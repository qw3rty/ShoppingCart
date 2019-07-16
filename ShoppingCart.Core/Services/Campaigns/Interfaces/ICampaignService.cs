using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces;

namespace ShoppingCart.Core.Services.Campaigns.Interfaces
{
    public interface ICampaignService:IService
    {
        CampaignDto Get(ICampaignDiscountRule getAmountDiscountRule);
    }
}