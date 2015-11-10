using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Commands;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Orders.com.DAL.InMemory;
using Peasy;
using Peasy.Extensions;
using Peasy.Core;
using Peasy.Rules;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.BLL.Tests.Commands
{
    [TestClass]
    public class DeleteOrderCommandTests
    {
        [TestMethod]
        public void Execution_should_fail_when_order_id_is_not_supplied()
        {
            var command = new DeleteOrderCommand(0, Mock.Of<IOrderDataProxy>(), Mock.Of<IOrderItemService>(), Mock.Of<ITransactionContext>());
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        #region State-Based Tests

        [TestMethod]
        public void Execution_deletes_order_and_associated_order_items_state_based()
        {
            var order = new Order() { OrderDate = DateTime.Now };
            var orderRepo = new OrderRepository(Mock.Of<ICustomerDataProxy>(), Mock.Of<IOrderItemDataProxy>());
            orderRepo.Clear();
            order = orderRepo.Insert(order);
            var orderItemRepo = new OrderItemRepository();
            orderItemRepo.Clear();
            orderItemRepo.Insert(new OrderItem { OrderID = order.ID, OrderStatusID = OrderStatusConstants.PENDING_STATUS });
            orderItemRepo.Insert(new OrderItem { OrderID = order.ID, OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS });
            orderItemRepo.Insert(new OrderItem { OrderID = order.ID, OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE });
            orderItemRepo.Insert(new OrderItem { OrderID = 2, OrderStatusID = OrderStatusConstants.PENDING_STATUS });
            var orderItemService = new OrderItemService(orderItemRepo, Mock.Of<IProductDataProxy>(), Mock.Of<IInventoryItemDataProxy>(), new TransactionContextStub());

            var command = new DeleteOrderCommand(order.ID, orderRepo, orderItemService, new TransactionContextStub());
            command.Execute();
            orderRepo.GetAll().ShouldBeEmpty();
            orderItemRepo.GetAll().Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task Execution_deletes_order_and_associated_order_items_state_based_async()
        {
            var order = new Order() { OrderDate = DateTime.Now };
            var orderRepo = new OrderRepository(Mock.Of<ICustomerDataProxy>(), Mock.Of<IOrderItemDataProxy>());
            orderRepo.Clear();
            order = orderRepo.Insert(order);
            var orderItemRepo = new OrderItemRepository();
            orderItemRepo.Clear();
            await orderItemRepo.InsertAsync(new OrderItem { OrderID = order.ID, OrderStatusID = OrderStatusConstants.PENDING_STATUS });
            await orderItemRepo.InsertAsync(new OrderItem { OrderID = order.ID, OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS });
            await orderItemRepo.InsertAsync(new OrderItem { OrderID = order.ID, OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE });
            await orderItemRepo.InsertAsync(new OrderItem { OrderID = 2, OrderStatusID = OrderStatusConstants.PENDING_STATUS });
            var orderItemService = new OrderItemService(orderItemRepo, Mock.Of<IProductDataProxy>(), Mock.Of<IInventoryItemDataProxy>(), new TransactionContextStub());

            var command = new DeleteOrderCommand(order.ID, orderRepo, orderItemService, new TransactionContextStub());
            await command.ExecuteAsync();
            orderRepo.GetAll().ShouldBeEmpty();
            orderItemRepo.GetAll().Count().ShouldBe(1);
        }

        [TestMethod]
        public void Execution_should_fail_when_order_is_associated_with_items_that_have_been_shipped_state_based()
        {
            var order = new Order() { OrderDate = DateTime.Now };
            var orderRepo = new OrderRepository(Mock.Of<ICustomerDataProxy>(), Mock.Of<IOrderItemDataProxy>());
            orderRepo.Clear();
            order = orderRepo.Insert(order);
            var orderItemRepo = new OrderItemRepository();
            orderItemRepo.Clear();
            orderItemRepo.Insert(new OrderItem() { OrderID = order.ID, OrderStatusID = OrderStatusConstants.PENDING_STATUS });
            2.Times(() => orderItemRepo.Insert(new OrderItem() { OrderID = order.ID, OrderStatusID = OrderStatusConstants.SHIPPED_STATUS }));
            var orderItemService = new OrderItemService(orderItemRepo, Mock.Of<IProductDataProxy>(), Mock.Of<IInventoryItemDataProxy>(), Mock.Of<ITransactionContext>());

            var command = new DeleteOrderCommand(order.ID, orderRepo, orderItemService, new TransactionContextStub());
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(2);
        }

        [TestMethod]
        public async Task Execution_should_fail_when_order_is_associated_with_items_that_have_been_shipped_state_based_async()
        {
            var order = new Order() { OrderDate = DateTime.Now };
            var orderRepo = new OrderRepository(Mock.Of<ICustomerDataProxy>(), Mock.Of<IOrderItemDataProxy>());
            orderRepo.Clear();
            order = await orderRepo.InsertAsync(order);
            var orderItemRepo = new OrderItemRepository();
            orderItemRepo.Clear();
            await orderItemRepo.InsertAsync(new OrderItem() { OrderID = order.ID, OrderStatusID = OrderStatusConstants.PENDING_STATUS });
            2.Times(async () => await orderItemRepo.InsertAsync(new OrderItem() { OrderID = order.ID, OrderStatusID = OrderStatusConstants.SHIPPED_STATUS }));
            var orderItemService = new OrderItemService(orderItemRepo, Mock.Of<IProductDataProxy>(), Mock.Of<IInventoryItemDataProxy>(), Mock.Of<ITransactionContext>());

            var command = new DeleteOrderCommand(order.ID, orderRepo, orderItemService, new TransactionContextStub());
            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(2);
        }

        #endregion

        #region Behavior-Based Tests

        [TestMethod]
        public void Execution_should_fail_when_order_is_associated_with_items_that_have_been_shipped()
        {
            var orderItems = new[]
            {
                new OrderItem() { ID = 1, OrderStatusID = OrderStatusConstants.PENDING_STATUS },
                new OrderItem() { ID = 2, OrderStatusID = OrderStatusConstants.SHIPPED_STATUS },
                new OrderItem() { ID = 3, OrderStatusID = OrderStatusConstants.SHIPPED_STATUS }
            };
            var orderID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByOrder(It.IsAny<long>()))
                              .Returns(orderItems);
            orderItemDataProxy.Setup(p => p.GetByID(It.IsAny<long>()))
                              .Returns((long i) => orderItems.First(oi => oi.ID == i));
            var command = new DeleteOrderCommand
                              (
                                  orderID,
                                  Mock.Of<IOrderDataProxy>(),
                                  new OrderItemService
                                  (
                                      orderItemDataProxy.Object,
                                      Mock.Of<IProductDataProxy>(),
                                      Mock.Of<IInventoryItemDataProxy>(),
                                      Mock.Of<ITransactionContext>()
                                  ),
                                  Mock.Of<ITransactionContext>()
                              );
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(2);
        }

        [TestMethod]
        public async Task Execution_should_fail_when_order_is_associated_with_items_that_have_been_shipped_async()
        {
            var orderItems = new[]
            {
                new OrderItem() { ID = 1, OrderStatusID = OrderStatusConstants.PENDING_STATUS },
                new OrderItem() { ID = 2, OrderStatusID = OrderStatusConstants.SHIPPED_STATUS },
                new OrderItem() { ID = 3, OrderStatusID = OrderStatusConstants.SHIPPED_STATUS }
            };
            var orderID = 1;
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByOrderAsync(It.IsAny<long>()))
                              .Returns(Task.FromResult<IEnumerable<OrderItem>>(orderItems));
            orderItemDataProxy.Setup(p => p.GetByIDAsync(It.IsAny<long>()))
                              .Returns(((long i) => Task.FromResult<OrderItem>(orderItems.First(oi => oi.ID == i))));
            var command = new DeleteOrderCommand
                              (
                                  orderID,
                                  Mock.Of<IOrderDataProxy>(),
                                  new OrderItemService
                                  (
                                      orderItemDataProxy.Object,
                                      Mock.Of<IProductDataProxy>(),
                                      Mock.Of<IInventoryItemDataProxy>(),
                                      Mock.Of<ITransactionContext>()
                                  ),
                                  Mock.Of<ITransactionContext>()
                              );
            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(2);
        }

        [TestMethod]
        public void Execution_deletes_order_and_associated_order_items()
        {
            var orders = new List<Order>()
            {
                new Order { ID = 1 }
            };
            var orderItems = new List<OrderItem>
            {
                new OrderItem { ID = 1, OrderID = 1, OrderStatusID = OrderStatusConstants.PENDING_STATUS },
                new OrderItem { ID = 2, OrderID = 1, OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS },
                new OrderItem { ID = 3, OrderID = 1, OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE },
                new OrderItem { ID = 4, OrderID = 2, OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE }
            };
            var deletedOrderItemIds = new List<long>();
            var orderID = 1;
            var orderDataProxy = new Mock<IOrderDataProxy>();
            orderDataProxy.Setup(p => p.Delete(orderID)).Callback((long id) => orders.Remove(id));
            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByOrder(It.IsAny<long>()))
                              .Returns((long i) => orderItems.Where(item => item.OrderID == i));
            orderItemDataProxy.Setup(p => p.GetByID(It.IsAny<long>()))
                              .Returns((long i) => orderItems.First(oi => oi.ID == i));
            orderItemDataProxy.Setup(p => p.Delete(It.IsAny<long>())).Callback((long id) => deletedOrderItemIds.Add(id));
            var command = new DeleteOrderCommand
                              (
                                  orderID,
                                  orderDataProxy.Object,
                                  new OrderItemService
                                  (
                                      orderItemDataProxy.Object,
                                      Mock.Of<IProductDataProxy>(),
                                      Mock.Of<IInventoryItemDataProxy>(),
                                      Mock.Of<ITransactionContext>()
                                  ),
                                  new TransactionContextStub()
                              );
            var result = command.Execute();
            result.Success.ShouldBe(true);
            orders.Count().ShouldBe(0);
            deletedOrderItemIds.ShouldBe(new long[] { 1, 2, 3 });
        }

        [TestMethod]
        public async Task Execution_deletes_order_and_associated_order_items_async()
        {
            var orders = new List<Order>()
            {
                new Order { ID = 1 }
            };
            var orderItems = new List<OrderItem>
            {
                new OrderItem { ID = 1, OrderID = 1, OrderStatusID = OrderStatusConstants.PENDING_STATUS },
                new OrderItem { ID = 2, OrderID = 1, OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS },
                new OrderItem { ID = 3, OrderID = 1, OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE },
                new OrderItem { ID = 4, OrderID = 2, OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE }
            };
            var deletedOrderItemIds = new List<long>();
            var orderID = 1;

            var orderDataProxy = new Mock<IOrderDataProxy>();
            orderDataProxy.Setup(p => p.DeleteAsync(orderID))
                          .Callback((long id) => orders.Remove(id))
                          .Returns(Task.Delay(0));

            var orderItemDataProxy = new Mock<IOrderItemDataProxy>();
            orderItemDataProxy.Setup(p => p.GetByOrderAsync(It.IsAny<long>()))
                              .Returns((long i) => Task.FromResult<IEnumerable<OrderItem>>(orderItems.Where(item => item.OrderID == i)));
            orderItemDataProxy.Setup(p => p.GetByIDAsync(It.IsAny<long>()))
                              .Returns((long i) => Task.FromResult<OrderItem>(orderItems.First(oi => oi.ID == i)));
            orderItemDataProxy.Setup(p => p.DeleteAsync(It.IsAny<long>()))
                              .Callback((long id) => deletedOrderItemIds.Add(id))
                              .Returns(Task.Delay(0));

            var command = new DeleteOrderCommand
                              (
                                  orderID,
                                  orderDataProxy.Object,
                                  new OrderItemService
                                  (
                                      orderItemDataProxy.Object,
                                      Mock.Of<IProductDataProxy>(),
                                      Mock.Of<IInventoryItemDataProxy>(),
                                      Mock.Of<ITransactionContext>()
                                  ),
                                  new TransactionContextStub()
                              );

            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(true);
            orders.Count().ShouldBe(0);
            deletedOrderItemIds.ShouldBe(new long[] { 1, 2, 3 });
        }
        #endregion
    }

    public static class ListExtensions
    {
        public static void Remove<T>(this List<T> list, long id) where T : IDomainObject<long>
        {
            list.RemoveAt(list.IndexOf(list.First(i => i.ID == id)));
        }
    }
}
