namespace ShoppingCart.Domain.Campaigns
{
    public class Campaign : DomainBase
    {
        public CampaignRule CampaignRule { get; set; }
    }

    public class CampaignRule
    {
        public CampaignRuleType CampaignRuleType { get; set; }
        public ICampaignRuleConditions RuleConditions { get; set; }
    }

    public interface ICampaignRuleConditions
    {
        int Quantity { get; set; }
    }

    public interface IAmountCampaignRuleConditions : ICampaignRuleConditions
    {
        double Price { get; set; }
    }

    public interface IRateCampaignRuleConditions : ICampaignRuleConditions
    {
        double Percentage { get; set; }
    }

    public enum CampaignRuleType
    {
        Rate = 10,
        Amount = 20,
    }
}