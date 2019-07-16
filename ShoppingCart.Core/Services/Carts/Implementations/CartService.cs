using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Components.Constants;
using ShoppingCart.Core.Components.Extensions;
using ShoppingCart.Core.Components.Helper;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Implementations;
using ShoppingCart.Core.Services.Carts.Interfaces;
using ShoppingCart.Core.Services.Delivery.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces;
using ShoppingCart.Domain.Carts;

namespace ShoppingCart.Core.Services.Carts.Implementations
{
    public class CartService : ServiceBase, ICartService
    {
        private double _fixedCost = 2.99;

        //todo: _costPerDelivery, _costPerProduct belirtilmemiş.
        private double _costPerDelivery = 1.1;
        private double _costPerProduct = 1.1;
        private IDeliveryService _deliveryService;
        private IDiscountService _discountService;

        public CartService(IDeliveryService deliveryService,
            IDiscountService discountService)
        {
            _deliveryService = deliveryService;
            _discountService = discountService;
        }

        public CartDto Get()
        {
            return new Cart().ToDto();
        }

        public void AddItem(CartDto cart, ProductDto product, int quantity)
        {
            ValidateAddItem(cart, product, quantity);
            for (var i = 0; i < quantity; i++)
            {
                cart.ProductList.Add(product);
            }
        }

        public string Print(CartDto cart)
        {
            var result =
                $"{PrintProductsByGroup(cart)}\r\n{PrintTotals(cart)}\r\n{PrintDiscounts(cart)}\r\n{PrintDiscountedTotal(cart)}\r\n{PrintDelivery(cart)}";
            Console.WriteLine(result);
            return result;
        }


        public double GetTotalAmountAfterDiscounts(CartDto cart)
        {
            return GetDiscountedTotal(cart);
        }

        public double GetCouponDiscount(CartDto cart)
        {
            return _discountService.CalculateCouponDiscount(cart.Coupon, GetTotal(cart),
                _discountService.CalculateCampaignDiscount(cart));
        }

        public double GetCampaignDiscount(CartDto cart)
        {
            return _discountService.CalculateCampaignDiscount(cart);
        }

        public double GetDeliveryDiscount(CartDto cart)
        {
            return CalculateDelivery(cart);
        }

        #region Util

        #region ValidateAddItem

        private void ValidateAddItem(CartDto cart, ProductDto product, int quantity)
        {
            ValidateAddItemCart(cart);
            ValidateAddItemProduct(product);
            ValidateAddItemQuantity(quantity);
        }

        private void ValidateAddItemCart(CartDto cart)
        {
            if (cart == null)
                throw new Exception(ResourceConstantsExceptions.NotFound("Cart"));
        }

        private void ValidateAddItemProduct(ProductDto product)
        {
            if (product == null)
                throw new Exception(ResourceConstantsExceptions.NotFound("Product"));
        }

        private void ValidateAddItemQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new Exception(ResourceConstantsExceptions.NotValid("Quantity"));
        }

        #endregion

        #region Print

        private string PrintProductsByGroup(CartDto cart)
        {
            var result = new List<string>();
            var group = cart.ProductList.GroupBy(x => x.Category);
            group.ToList().ForEach(x =>
            {
                result.Add($"Category Name : {x.Key.Title}");
                result.Add(PrintProducts(cart.ProductList.Where(y => y.Category.Title == x.Key.Title).ToList()));
            });
            return result.ToNewLineString();
        }

        private string PrintProducts(IList<ProductDto> productDtoList)
        {
            var result = new List<string>();
            productDtoList.GroupBy(x => x).ToList().ForEach(x =>
            {
                result.Add($"Product Name : {x.Key.Title}\t" +
                           $"Quantity : {x.Count().ToString()}\t" +
                           $"Unit Price : {x.Key.Price:N2}\t" +
                           $"Total Price : {x.Key.Price * x.Count():N2}");
            });
            return result.ToNewLineString();
        }

        private string PrintTotals(CartDto cart)
        {
            return $"Total Price : {GetTotal(cart):N2}";
        }

        private string PrintDiscountedTotal(CartDto cart)
        {
            return $"Discounted Total : {GetDiscountedTotal(cart):N2}";
        }

        private string PrintDiscounts(CartDto cart)
        {
            return
                $"Discount Total : {GetDiscounts(cart):N2}\t" +
                $"(Coupon : {GetCouponDiscount(cart):N2}\tCampaign : {GetCampaignDiscount(cart)})";
        }

        private string PrintDelivery(CartDto cart)
        {
            return $"Delivery Cost : {CalculateDelivery(cart):N2}";
        }

        #endregion

        #region Totals

        private double GetDiscountedTotal(CartDto cart)
        {
            return GetTotal(cart) - GetDiscounts(cart);
        }

        private double GetDiscounts(CartDto cart)
        {
            var cartTotal = GetTotal(cart);
            var campaignDiscount = _discountService.CalculateCampaignDiscount(cart);
            var discountTotal = campaignDiscount +
                                _discountService.CalculateCouponDiscount(cart.Coupon, cartTotal, campaignDiscount);
            return discountTotal >= cartTotal ? cartTotal : discountTotal;
        }

        private double GetTotal(CartDto cart)
        {
            return cart.ProductList.Sum(x => x.Price);
        }

        #endregion

        #region Discounts

        private double CalculateDelivery(CartDto cart)
        {
            return _deliveryService.Calculate(cart, _costPerDelivery, _costPerProduct, _fixedCost);
        }

        #endregion

        #endregion
    }
}