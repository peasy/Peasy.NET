using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders.com.BLL.Rules;
using Orders.com.Core.Domain;
using Peasy.Rules;
using Shouldly;
using System.Threading.Tasks;

namespace Orders.com.BLL.Tests.Rules
{
    [TestClass]
    public class OrderItemAmountValidityRuleTests
    {
        [TestMethod]
        public void Is_valid_when_item_amount_equals_item_quantity_times_product_price()
        {
            var orderItem = new OrderItem() { Amount = 30, Quantity = 3 };
            var product = new Product { Price = 10 };
            var rule = new OrderItemAmountValidityRule(orderItem, product);
            rule.Validate().IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public void Is_invalid_when_item_amount_does_not_equal_item_amount_times_product_price()
        {
            var orderItem = new OrderItem() { Amount = 30, Quantity = 3 };
            var product = new Product { Price = 11 };
            var rule = new OrderItemAmountValidityRule(orderItem, product);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public async Task Is_valid_when_item_amount_equals_item_quantity_times_product_price_async()
        {
            var orderItem = new OrderItem() { Amount = 30, Quantity = 3 };
            var product = new Product { Price = 10 };
            var rule = new OrderItemAmountValidityRule(orderItem, product);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public async Task Is_invalid_when_item_amount_does_not_equal_item_amount_times_product_price_async()
        {
            var orderItem = new OrderItem() { Amount = 30, Quantity = 3 };
            var product = new Product { Price = 11 };
            var rule = new OrderItemAmountValidityRule(orderItem, product);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }
    }
}
