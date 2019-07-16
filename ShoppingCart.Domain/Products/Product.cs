using ShoppingCart.Domain.Categories;

namespace ShoppingCart.Domain.Products
{
    public class Product : DomainBase
    {
        public Product(string title, double price)
        {
            Title = title;
            Price = price;
        }
        public Category Category { get; set; }

        public string Title { get; set; }
        public double Price { get; set; }
    }
}