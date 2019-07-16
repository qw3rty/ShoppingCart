namespace ShoppingCart.Core.Services.Discounts.Interfaces.DiscountRules
{
    public interface IRateCampaignDiscountRule : ICampaignDiscountRule
    {
        double Percentage { get; set; }
    }
}