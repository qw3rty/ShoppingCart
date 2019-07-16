using System.Collections.Generic;

namespace ShoppingCart.Core.Dtos.Responses
{
    public class CategoryDto
    {
        public string Title { get; set; }
        public CategoryDto Parent { get; set; }
        public IList<CampaignDto> CampaignList { get; set; }
    }
}