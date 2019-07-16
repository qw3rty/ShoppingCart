using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Components.Constants;
using ShoppingCart.Core.Components.Extensions;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Implementations;
using ShoppingCart.Core.Services.Categories.Interfaces;
using ShoppingCart.Domain.Categories;

namespace ShoppingCart.Core.Services.Categories.Implementations
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryDto Get(string title, List<CampaignDto> campaigns = null)
        {
            return Get(title, null, campaigns);
        }

        public CategoryDto Get(string title, CategoryDto category, List<CampaignDto> campaigns = null)
        {
            ValidateGet(title);
            return new Category(title, category.ToEntity(), campaigns?.Select(x => x.ToEntity()).ToList()).ToDto();
        }


        #region Utils

        #region ValidateGet

        private void ValidateGet(string title)
        {
            if (title.Length < 1)
                throw new Exception(ResourceConstantsExceptions.NotValid("Name"));
        }

        #endregion

        #endregion
    }
}