namespace ShoppingCart.Domain.Campaigns
{
    public class RateCampaignRuleConditions : IRateCampaignRuleConditions
    {
        public RateCampaignRuleConditions(double percentage, int quantity)
        {
            Percentage = percentage;
            Quantity = quantity;
        }
        public int Quantity { get; set; }
        public double Percentage { get; set; }
    }
}