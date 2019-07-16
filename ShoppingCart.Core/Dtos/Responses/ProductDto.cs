using System.Collections.Generic;

namespace ShoppingCart.Core.Dtos.Responses
{
    public class ProductDto
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public CategoryDto Category { get; set; }
        public IList<CategoryDto> CategoryList => GetCategoryList(Category);

        private IList<CategoryDto> GetCategoryList(CategoryDto category)
        {
            var result = new List<CategoryDto>();
            result.Add(category);
            if (category.Parent != null)
                result.AddRange(GetCategoryList(category.Parent));
            return result;
        }
    }
}