namespace ShoppingCart.Domain.Campaigns
{
    public class AmountCampaignRuleConditions : IAmountCampaignRuleConditions
    {
        public AmountCampaignRuleConditions(double price, int quantity)
        {
            Price = price;
            Quantity = quantity;
        }

        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}