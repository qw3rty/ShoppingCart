namespace ShoppingCart.Core.Services.Discounts.Interfaces.DiscountRules
{
    public interface IAmountCampaignDiscountRule : ICampaignDiscountRule
    {
        double Price { get; set; }
    }
}