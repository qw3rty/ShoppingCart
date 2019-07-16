using System.Collections.Generic;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Interfaces;

namespace ShoppingCart.Core.Services.Categories.Interfaces
{
    public interface ICategoryService : IService
    {
        CategoryDto Get(string title, List<CampaignDto> campaigns = null);
        CategoryDto Get(string title, CategoryDto category, List<CampaignDto> campaigns = null);
    }
}