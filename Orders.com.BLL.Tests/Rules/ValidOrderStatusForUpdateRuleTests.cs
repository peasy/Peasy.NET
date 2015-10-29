using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Peasy;
using Peasy.Rules;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.BLL.Tests.Rules
{
    [TestClass]
    public class ValidOrderStatusForUpdateRuleTests
    {
        [TestMethod]
        public void Is_invalid_when_order_has_any_associated_order_items_that_have_been_shipped()
        {
            var orderID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByOrder(orderID))
                              .Returns((new[]
                                      {
                                         new OrderItem() { OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE },
                                         new OrderItem() { OrderStatusID = OrderStatusConstants.SHIPPED_STATUS }
                                      }).AsEnumerable<OrderItem>());
                                                                    
            var service = new OrderItemService(orderItemDataProxy.Object, 
                                               Mock.Of<IProductDataProxy>(), 
                                               Mock.Of<IInventoryItemDataProxy>(), 
                                               Mock.Of<ITransactionContext>());

            var rule = new ValidOrderStatusForUpdateRule(orderID, service);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public void Is_valid_when_order_has_no_associated_order_items_that_have_been_shipped()
        {
            var orderID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByOrder(orderID))
                              .Returns((new[]
                                      {
                                         new OrderItem() { OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE },
                                         new OrderItem() { OrderStatusID = OrderStatusConstants.PENDING_STATUS }
                                      }).AsEnumerable<OrderItem>());
                                                                    
            var service = new OrderItemService(orderItemDataProxy.Object, 
                                               Mock.Of<IProductDataProxy>(), 
                                               Mock.Of<IInventoryItemDataProxy>(), 
                                               Mock.Of<ITransactionContext>());

            var rule = new ValidOrderStatusForUpdateRule(orderID, service);
            rule.Validate().IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public async Task Is_invalid_when_order_has_any_associated_order_items_that_have_been_shipped_async()
        {
            var orderID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByOrderAsync(orderID))
                              .Returns(Task.FromResult((new[]
                                      {
                                         new OrderItem() { OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE },
                                         new OrderItem() { OrderStatusID = OrderStatusConstants.SHIPPED_STATUS }
                                      }).AsEnumerable<OrderItem>()));
                                                                    
            var service = new OrderItemService(orderItemDataProxy.Object, 
                                               Mock.Of<IProductDataProxy>(), 
                                               Mock.Of<IInventoryItemDataProxy>(), 
                                               Mock.Of<ITransactionContext>());

            var rule = new ValidOrderStatusForUpdateRule(orderID, service);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public async Task Is_valid_when_order_has_no_associated_order_items_that_have_been_shipped_async()
        {
            var orderID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByOrderAsync(orderID))
                              .Returns(Task.FromResult((new[]
                                      {
                                         new OrderItem() { OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE },
                                         new OrderItem() { OrderStatusID = OrderStatusConstants.PENDING_STATUS }
                                      }).AsEnumerable<OrderItem>()));
                                                                    
            var service = new OrderItemService(orderItemDataProxy.Object, 
                                               Mock.Of<IProductDataProxy>(), 
                                               Mock.Of<IInventoryItemDataProxy>(), 
                                               Mock.Of<ITransactionContext>());

            var rule = new ValidOrderStatusForUpdateRule(orderID, service);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }
    }
}
