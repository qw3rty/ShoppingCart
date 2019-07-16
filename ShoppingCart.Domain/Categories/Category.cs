using System.Collections.Generic;
using ShoppingCart.Domain.Campaigns;

namespace ShoppingCart.Domain.Categories
{
    public class Category : DomainBase
    {
        public Category(string title, Category parent = null, IList<Campaign> campaignList = null)
        {
            Title = title;
            Parent = parent;
            CampaignList = campaignList ?? new List<Campaign>();
        }

        public string Title { get; set; }
        public Category Parent { get; set; }
        public IList<Campaign> CampaignList { get; set; }
    }
}