using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Implementations;
using ShoppingCart.Core.Services.Discounts.Implementations.DiscountRules;
using ShoppingCart.Core.Services.Discounts.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces.DiscountRules;

namespace ShoppingCart.Core.Services.Discounts.Implementations
{
    public class DiscountService : ServiceBase, IDiscountService
    {
        public ICampaignDiscountRule GetRateCampaignDiscountRule(double percentage, int quantity)
        {
            return new RateCampaignDiscountRule(percentage, quantity);
        }

        public ICampaignDiscountRule GetAmountCampaignDiscountRule(double price, int quantity)
        {
            return new AmountCampaignDiscountRule(price, quantity);
        }

        public ICouponDiscountRule GetAmountCouponDiscountRule(double percentage, double minPurchaseAmount)
        {
            return new RateCouponDiscountRule(percentage, minPurchaseAmount);
        }

        public double CalculateCampaignDiscount(CartDto cart)
        {
            var discountList = new List<CampaignDiscountDto>();
            cart.ProductList.ToList().ForEach(product =>
                {
                    var rules = GetDiscountRuleList(product, cart.ProductList);
                    var discount = CalculateDiscount(product, rules.ToList(), cart.ProductList);
                    discountList.AddRange(discount);
                }
            );
            return GetMaximumAmountDiscount(discountList);
        }


        public double CalculateCouponDiscount(ICouponDiscountRule cartCoupon, double cartTotal, double campaignDiscount)
        {
            if (cartCoupon == null || campaignDiscount >= cartTotal) return 0;

            var coupon = ((IRateCouponDiscountRule) cartCoupon);
            if (cartTotal - campaignDiscount >= coupon.MinPurchaseAmount)
                return (cartTotal - campaignDiscount) * coupon.Percentage / 100;

            return 0;
        }

        private double GetMaximumAmountDiscount(IList<CampaignDiscountDto> discountList)
        {
            var maxAmountCampaign = GetMaxAmountCampaign(discountList);
            var sumRateCampaign = GetSumRateCampaign(discountList);
            return maxAmountCampaign > sumRateCampaign ? maxAmountCampaign : sumRateCampaign;
        }

        private double GetMaxAmountCampaign(IList<CampaignDiscountDto> discountList)
        {
            return discountList.Where(x => x.Rule is IAmountCampaignDiscountRule).Max(x => x.Discount);
        }

        private double GetSumRateCampaign(IList<CampaignDiscountDto> discountList)
        {
            return discountList.Where(x => x.Rule is IRateCampaignDiscountRule).Sum(x => x.Discount);
        }

        private IList<ICampaignDiscountRule> GetDiscountRuleList(ProductDto product,
            IList<ProductDto> productList)
        {
            var ruleList = new List<ICampaignDiscountRule>();
            product.CategoryList.ToList().ForEach(x =>
            {
                var rule = GetDiscountRuleList(x,
                    productList.Where(y => y.CategoryList.Select(z => z.Title).Contains(x.Title)).ToList());
                if (rule != null) ruleList.AddRange(rule);
            });
            return ruleList;
        }

        private IList<ICampaignDiscountRule> GetDiscountRuleList(CategoryDto category,
            IList<ProductDto> productList)
        {
            var ruleList = new List<ICampaignDiscountRule>();
            category.CampaignList.ToList().ForEach(x =>
            {
                var rule = GetCampaignDiscountRule(x.CampaignDiscountRule, productList.ToList());
                if (rule != null) ruleList.Add(rule);
            });
            return ruleList;
        }

        private ICampaignDiscountRule GetCampaignDiscountRule(ICampaignDiscountRule rule,
            IList<ProductDto> productList)
        {
            var result = rule.GetDiscountRule(productList);
            return result;
        }

        private IList<CampaignDiscountDto> CalculateDiscount(ProductDto productDto,
            List<ICampaignDiscountRule> ruleList, IList<ProductDto> productList)
        {
            var result = new List<CampaignDiscountDto>();
            double currentDiscount = 0;
            ruleList.Where(z => z is IRateCampaignDiscountRule).Distinct().ToList().ForEach(x =>
            {
                var discount = x.CalculateCampaignDiscount(productDto, productList);
                if (discount > 0 && discount > currentDiscount)
                {
                    result.Remove(result.FirstOrDefault(z =>
                        z.Rule.GetType().Name == nameof(RateCampaignDiscountRule) && z.Discount == currentDiscount));
                    result.Add(new CampaignDiscountDto {Rule = x, Discount = discount});
                    currentDiscount = discount;
                }
            });
            currentDiscount = 0;
            ruleList.Where(z => z is IAmountCampaignDiscountRule).Distinct().ToList().ForEach(x =>
            {
                var discount = x.CalculateCampaignDiscount(productDto, productList);
                if (discount > 0 && discount > currentDiscount)
                {
                    result.Remove(result.FirstOrDefault(z =>
                        z.Rule.GetType().Name == nameof(AmountCampaignDiscountRule) && z.Discount == currentDiscount));
                    result.Add(new CampaignDiscountDto {Rule = x, Discount = discount});
                    currentDiscount = discount;
                }
            });
            return result;
        }
    }
}