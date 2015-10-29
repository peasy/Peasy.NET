using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions; 
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Peasy;

namespace Orders.com.BLL.Tests.Rules
{
    [TestClass]
    public class CanSubmitOrderItemRuleTests
    {
        [TestMethod]
        public void Is_invalid_when_an_order_item_is_backordered()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE;
            var rule = new CanSubmitOrderItemRule(item);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public void Is_invalid_when_an_order_item_is_submitted()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS;
            var rule = new CanSubmitOrderItemRule(item);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public void Is_invalid_when_an_order_item_is_pending()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
            var rule = new CanSubmitOrderItemRule(item);
            rule.Validate().IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public void Is_invalid_when_an_order_item_is_shipped()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
            var rule = new CanSubmitOrderItemRule(item);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public async Task Is_invalid_when_an_order_item_is_backordered_async()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE;
            var rule = new CanSubmitOrderItemRule(item);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public async Task Is_invalid_when_an_order_item_is_submitted_async()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS;
            var rule = new CanSubmitOrderItemRule(item);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public async Task Is_valid_when_an_order_item_is_pending_async()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
            var rule = new CanSubmitOrderItemRule(item);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public async Task Is_invalid_when_an_order_item_is_shipped_async()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
            var rule = new CanSubmitOrderItemRule(item);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }
    }
}
