using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders.com.BLL.Rules;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Peasy.Rules;
using Shouldly;
using System.Threading.Tasks;

namespace Orders.com.BLL.Tests.Rules
{
    [TestClass]
    public class ValidOrderItemStatusForUpdateRuleTests
    {
        [TestMethod]
        public void Is_valid_when_item_status_is_pending()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
            var rule = new ValidOrderItemStatusForUpdateRule(item);
            rule.Validate().IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public void Is_valid_when_item_status_is_submitted()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS;
            var rule = new ValidOrderItemStatusForUpdateRule(item);
            rule.Validate().IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public void Is_invalid_when_item_status_is_shipped()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
            var rule = new ValidOrderItemStatusForUpdateRule(item);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("Shipped items cannot be changed");
        }

        [TestMethod]
        public void Is_invalid_when_item_status_is_backordered()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE;
            var rule = new ValidOrderItemStatusForUpdateRule(item);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("Backordered items cannot be changed");
        }

        [TestMethod]
        public async Task Is_valid_when_item_status_is_pending_async()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
            var rule = new ValidOrderItemStatusForUpdateRule(item);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public async Task Is_valid_when_item_status_is_submitted_async()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS;
            var rule = new ValidOrderItemStatusForUpdateRule(item);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public async Task Is_invalid_when_item_status_is_shipped_async()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
            var rule = new ValidOrderItemStatusForUpdateRule(item);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("Shipped items cannot be changed");
        }

        [TestMethod]
        public async Task Is_invalid_when_item_status_is_backordered_async()
        {
            var item = new OrderItem();
            item.OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE;
            var rule = new ValidOrderItemStatusForUpdateRule(item);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("Backordered items cannot be changed");
        }

        //[TestMethod]
        //public async Task Is_valid_when_item_status_is_not_shipped_async()
        //{
        //    var item = new OrderItem();
        //    item.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
        //    var rule = new ValidOrderItemStatusForDeleteRule(item);
        //    await rule.ValidateAsync();
        //    rule.IsValid.ShouldBe(true);
        //    rule.ErrorMessage.ShouldBe(null);
        //}

        //[TestMethod]
        //public async Task Is_invalid_when_item_status_is_shipped_async()
        //{
        //    var item = new OrderItem();
        //    item.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
        //    var rule = new ValidOrderItemStatusForDeleteRule(item);
        //    await rule.ValidateAsync();
        //    rule.IsValid.ShouldBe(false);
        //    rule.ErrorMessage.ShouldNotBe(null);
        //}
    }
}
