using System.Collections.Generic;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Base.Implementations;
using ShoppingCart.Core.Services.Base.Interfaces;
using ShoppingCart.Core.Services.Campaigns.Interfaces;
using ShoppingCart.Core.Services.Carts.Interfaces;
using ShoppingCart.Core.Services.Categories.Interfaces;
using ShoppingCart.Core.Services.Coupons.Interfaces;
using ShoppingCart.Core.Services.Discounts.Interfaces;
using ShoppingCart.Core.Services.Products.Interfaces;

namespace ShoppingCart.Test
{
    [TestFixture]
    public class ProductTests
    {
        #region Properties

        private static IWindsorContainer _container;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private ICartService _cartService;
        private ICampaignService _campaignService;
        private IDiscountService _discountService;
        private ICouponService _couponService;

        #endregion

        [OneTimeSetUp]
        public void Setup()
        {
            _container = new WindsorContainer();
            _container.Register(
                Types.FromAssemblyContaining<ServiceBase>().BasedOn<IService>().WithServiceAllInterfaces(),
                Types.FromAssemblyContaining<ServiceBase>().BasedOn<IInterceptor>()
            );
            _productService = _container.Resolve<IProductService>();
            _categoryService = _container.Resolve<ICategoryService>();
            _cartService = _container.Resolve<ICartService>();
            _campaignService = _container.Resolve<ICampaignService>();
            _discountService = _container.Resolve<IDiscountService>();
            _couponService = _container.Resolve<ICouponService>();
        }

        [Test]
        public void TestGetCategory()
        {
            var category = _categoryService.Get("food");
            Assert.True(category != null);
        }

        [Test]
        public void TestGetProduct()
        {
            var category = _categoryService.Get("food");
            var product = _productService.Get("Apple", 100, category);
            Assert.True(product != null);
        }

        [Test]
        public void TestGetCart()
        {
            var cart = _cartService.Get();
            Assert.True(cart != null);
        }

        [Test]
        public void TestAddProductToCart()
        {
            var category = _categoryService.Get("food");
            var product = _productService.Get("Apple", 100, category);
            var cart = _cartService.Get();
            _cartService.AddItem(cart, product, 5);

            Assert.True(product != null);
        }

        [Test]
        public void TestAddCampaingToProduct()
        {
            var category = _categoryService.Get("food", TestCampaigns());
            var product = _productService.Get("Apple", 100, category);

            Assert.True(product.Category.CampaignList.Count > 0);
        }

        [Test]
        public void TestAddCouponToCart()
        {
            var category = _categoryService.Get("food");

            var product = _productService.Get("Apple", 100, category);
            var cart = _cartService.Get();
            _cartService.AddItem(cart, product, 5);

            var coupon = _discountService.GetAmountCouponDiscountRule(100, 10);
            _couponService.AddCoupon(cart, coupon);

            Assert.True(cart.Coupon != null);
        }

        [Test]
        public void TestPrint()
        {
            var cart = GetTestCartDto();
            var printResult = _cartService.Print(cart);
            Assert.True(!string.IsNullOrEmpty(printResult));
        }
        [Test]
        public void TestGetTotalAmountAfterDiscounts()
        {
            var cart = GetTestCartDto();
            var totalAmountAfterDiscounts = _cartService.GetTotalAmountAfterDiscounts(cart);
            Assert.True(totalAmountAfterDiscounts > 0);
        }

        [Test]
        public void TestGetCouponDiscount()
        {
            var cart = GetTestCartDto();
            var couponDiscount = _cartService.GetCouponDiscount(cart);
            Assert.True(couponDiscount > 0);
        }
        [Test]
        public void TestGetGetCampaignDiscount()
        {
            var cart = GetTestCartDto();
            var couponDiscount = _cartService.GetCampaignDiscount(cart);
            Assert.True(couponDiscount > 0);
        }

        [Test]
        public void TestGetDeliveryDiscount()
        {
            var cart = GetTestCartDto();
            var deliveryDiscount = _cartService.GetDeliveryDiscount(cart);
            Assert.True(deliveryDiscount > 0);
        }


        #region Utils

        private CartDto GetTestCartDto()
        {
            var category1 = _categoryService.Get("food", TestCampaigns());
            var category2 = _categoryService.Get("fruit", category1, TestCampaigns());

            var product = _productService.Get("Apple", 100, category1);
            var product2 = _productService.Get("Almonds", 150, category2);
            var product3 = _productService.Get("Banana", 50, category1);

            var cart = _cartService.Get();
            _cartService.AddItem(cart, product, 3);
            _cartService.AddItem(cart, product2, 1);
            _cartService.AddItem(cart, product3, 5);
            
            var coupon = _discountService.GetAmountCouponDiscountRule(10, 100);
            _couponService.AddCoupon(cart, coupon);
            return cart;
        }

        private List<CampaignDto> TestCampaigns()
        {
            return new List<CampaignDto>
            {
                _campaignService.Get(_discountService.GetRateCampaignDiscountRule(20, 3)),
                _campaignService.Get(_discountService.GetRateCampaignDiscountRule(50, 5)),
                _campaignService.Get(_discountService.GetAmountCampaignDiscountRule(5, 5))
            };
        }

        private List<CampaignDto> TestCampaigns2()
        {
            return new List<CampaignDto>
            {
                _campaignService.Get(_discountService.GetAmountCampaignDiscountRule(500, 5))
            };
        }

        #endregion
    }
}