using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Commands;
using Orders.com.DataProxy;
using Orders.com.Domain;
using Orders.com.Extensions;
using Peasy;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.BLL.Tests.Commands
{
    [TestClass]
    public class ShipOrderItemCommandTests
    {
        [TestMethod]
        public void Execution_should_fail_when_supplied_order_item_is_in_a_pending_state()
        {
            var orderItemID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByID(orderItemID))
                              .Returns(new OrderItem { OrderStatusID = OrderStatusConstants.PENDING_STATUS });
            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, Mock.Of<IInventoryItemDataProxy>(), Mock.Of<ITransactionContext>());
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public void Execution_should_fail_when_supplied_order_item_is_in_a_shipped_state()
        {
            var orderItemID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByID(orderItemID))
                              .Returns(new OrderItem { OrderStatusID = OrderStatusConstants.SHIPPED_STATUS });
            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, Mock.Of<IInventoryItemDataProxy>(), Mock.Of<ITransactionContext>());
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public void Execution_should_fail_when_supplied_order_item_has_no_status()
        {
            var orderItemID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByID(orderItemID))
                              .Returns(new OrderItem { OrderStatusID = 0 });
            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, Mock.Of<IInventoryItemDataProxy>(), Mock.Of<ITransactionContext>());
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public void Execution_reduces_inventory_and_sets_order_item_to_a_shipped_state()
        {
            var productID = 1;
            var orderItemID = 1;
            var inventoryItem = new InventoryItem { QuantityOnHand = 5, ProductID = productID };
            var orderItem = new OrderItem { ProductID = productID, Quantity = 5, OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS, ShippedDate = null, BackorderedDate = null };

            var inventoryDataProxy = new Mock<IInventoryItemDataProxy>();
            inventoryDataProxy.Setup(p => p.GetByProduct(productID)).Returns(inventoryItem);
            inventoryDataProxy.Setup(p => p.Update(It.IsAny<InventoryItem>()))
                              .Callback<InventoryItem>((item) => inventoryItem = item);
            
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByID(orderItemID)).Returns(orderItem);
            orderItemDataProxy.Setup(p => p.Ship(It.IsAny<OrderItem>())).Returns(orderItem);

            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, inventoryDataProxy.Object, new TransactionContextStub());
            var result = command.Execute();

            orderItem = result.Value;
            orderItem.ShippedDate.Value.Date.ShouldBe(DateTime.Now.ToUniversalTime().Date);
            orderItem.OrderStatus().IsShipped.ShouldBeTrue();
            orderItem.BackorderedDate.ShouldBeNull();
            inventoryItem.QuantityOnHand.ShouldBe(0);
        }

        [TestMethod]
        public void Execution_does_not_reduce_inventory_and_sets_order_item_to_a_backordered_state()
        {
            var productID = 1;
            var orderItemID = 1;
            var inventoryItem = new InventoryItem { QuantityOnHand = 5, ProductID = productID };
            var orderItem = new OrderItem { ProductID = productID, Quantity = 6, OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS, ShippedDate = null, BackorderedDate = null };

            var inventoryDataProxy = new Mock<IInventoryItemDataProxy>();
            inventoryDataProxy.Setup(p => p.GetByProduct(productID)).Returns(inventoryItem);
            inventoryDataProxy.Setup(p => p.Update(It.IsAny<InventoryItem>()))
                              .Callback<InventoryItem>((item) => inventoryItem = item);
            
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByID(orderItemID)).Returns(orderItem);
            orderItemDataProxy.Setup(p => p.Ship(It.IsAny<OrderItem>())).Returns(orderItem);

            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, inventoryDataProxy.Object, new TransactionContextStub());
            var result = command.Execute();

            orderItem = result.Value;
            orderItem.ShippedDate.ShouldBeNull();
            orderItem.OrderStatus().IsBackordered.ShouldBeTrue();
            orderItem.BackorderedDate.Value.Date.ShouldBe(DateTime.Now.ToUniversalTime().Date);
            inventoryItem.QuantityOnHand.ShouldBe(5);
        }

        [TestMethod]
        public async Task Execution_should_fail_when_supplied_order_item_is_in_a_pending_state_async()
        {
            var orderItemID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByIDAsync(orderItemID))
                              .Returns(Task.FromResult(new OrderItem { OrderStatusID = OrderStatusConstants.PENDING_STATUS }));
            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, Mock.Of<IInventoryItemDataProxy>(), Mock.Of<ITransactionContext>());
            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task Execution_should_fail_when_supplied_order_item_is_in_a_shipped_state_async()
        {
            var orderItemID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByIDAsync(orderItemID))
                              .Returns(Task.FromResult(new OrderItem { OrderStatusID = OrderStatusConstants.SHIPPED_STATUS }));
            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, Mock.Of<IInventoryItemDataProxy>(), Mock.Of<ITransactionContext>());
            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task Execution_should_fail_when_supplied_order_item_has_no_status_async()
        {
            var orderItemID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByIDAsync(orderItemID))
                              .Returns(Task.FromResult(new OrderItem { OrderStatusID = 0 }));
            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, Mock.Of<IInventoryItemDataProxy>(), Mock.Of<ITransactionContext>());
            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task Execution_reduces_inventory_and_sets_order_item_to_a_shipped_state_async()
        {
            var productID = 1;
            var orderItemID = 1;
            var inventoryItem = new InventoryItem { QuantityOnHand = 5, ProductID = productID };
            var orderItem = new OrderItem { ProductID = productID, Quantity = 5, OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS, ShippedDate = null, BackorderedDate = null };

            var inventoryDataProxy = new Mock<IInventoryItemDataProxy>();
            inventoryDataProxy.Setup(p => p.GetByProductAsync(productID)).Returns(Task.FromResult(inventoryItem));
            inventoryDataProxy.Setup(p => p.UpdateAsync(It.IsAny<InventoryItem>()))
                              .Returns(Task.FromResult(inventoryItem))
                              .Callback<InventoryItem>((item) => inventoryItem = item);
            
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByIDAsync(orderItemID)).Returns(Task.FromResult(orderItem));
            orderItemDataProxy.Setup(p => p.ShipAsync(It.IsAny<OrderItem>())).Returns(Task.FromResult(orderItem));

            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, inventoryDataProxy.Object, new TransactionContextStub());
            var result = await command.ExecuteAsync();

            orderItem = result.Value;
            orderItem.ShippedDate.Value.Date.ShouldBe(DateTime.Now.ToUniversalTime().Date);
            orderItem.OrderStatus().IsShipped.ShouldBeTrue();
            orderItem.BackorderedDate.ShouldBeNull();
            inventoryItem.QuantityOnHand.ShouldBe(0);
        }

        [TestMethod]
        public async Task Execution_does_not_reduce_inventory_and_sets_order_item_to_a_backordered_state_async()
        {
            var productID = 1;
            var orderItemID = 1;
            var inventoryItem = new InventoryItem { QuantityOnHand = 5, ProductID = productID };
            var orderItem = new OrderItem { ProductID = productID, Quantity = 6, OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS, ShippedDate = null, BackorderedDate = null };

            var inventoryDataProxy = new Mock<IInventoryItemDataProxy>();
            inventoryDataProxy.Setup(p => p.GetByProductAsync(productID)).Returns(Task.FromResult(inventoryItem));
            inventoryDataProxy.Setup(p => p.UpdateAsync(It.IsAny<InventoryItem>()))
                              .Callback<InventoryItem>((item) => inventoryItem = item);
            
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByIDAsync(orderItemID)).Returns(Task.FromResult(orderItem));
            orderItemDataProxy.Setup(p => p.ShipAsync(It.IsAny<OrderItem>())).Returns(Task.FromResult(orderItem));

            var command = new ShipOrderItemCommand(orderItemID, orderItemDataProxy.Object, inventoryDataProxy.Object, new TransactionContextStub());
            var result = await command.ExecuteAsync();

            orderItem = result.Value;
            orderItem.ShippedDate.ShouldBeNull();
            orderItem.OrderStatus().IsBackordered.ShouldBeTrue();
            orderItem.BackorderedDate.Value.Date.ShouldBe(DateTime.Now.ToUniversalTime().Date);
            inventoryItem.QuantityOnHand.ShouldBe(5);
        }
    }
}
