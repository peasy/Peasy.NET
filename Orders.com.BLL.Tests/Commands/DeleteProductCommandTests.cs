using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Commands;
using Orders.com.DataProxy;
using Orders.com.Domain;
using Orders.com.DAL.InMemory;
using Peasy;
using Peasy.Extensions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL.Tests.Commands
{
    [TestClass]
    public class DeleteProductCommandTests
    {
        [TestMethod]
        public void Execution_should_fail_when_product_id_greater_than_0_is_not_supplied()
        {
            var command = new DeleteProductCommand(0, Mock.Of<IProductDataProxy>(), Mock.Of<IInventoryItemService>(), Mock.Of<IOrderDataProxy>(), Mock.Of<ITransactionContext>());
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public void Execution_should_fail_when_product_is_associated_with_orders_items()
        {
            var orderProxy = new Mock<IOrderDataProxy>();
            orderProxy.Setup(proxy => proxy.GetByProduct(1)).Returns(new[] { new Order() });

            var command = new DeleteProductCommand(1, Mock.Of<IProductDataProxy>(),
                                                      Mock.Of<IInventoryItemService>(),
                                                      orderProxy.Object, 
                                                      Mock.Of<ITransactionContext>());
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public void Execution_deletes_product_and_associated_inventory_items()
        {
            var orderProxy = new Mock<IOrderDataProxy>();
            orderProxy.Setup(proxy => proxy.GetByProduct(1)).Returns(Enumerable.Empty<Order>());

            var product = new Product() { ProductID = 1 };
            var productDataProxy = new ProductRepository();
            productDataProxy.Clear();
            productDataProxy.Insert(product);

            var inventoryDataProxy = new InventoryItemRepository();
            inventoryDataProxy.Clear();
            inventoryDataProxy.Insert(new InventoryItem() { ProductID = 1 });

            var command = new DeleteProductCommand(1, productDataProxy,
                                                      new InventoryItemService(inventoryDataProxy), 
                                                      orderProxy.Object,
                                                      new TransactionContextStub());
            var result = command.Execute();
            result.Success.ShouldBe(true);
            result.Errors.ShouldBe(null);
            productDataProxy.GetAll().Count().ShouldBe(0);
            inventoryDataProxy.GetAll().Count().ShouldBe(0);
        }

        [TestMethod]
        public async Task Execution_should_fail_when_product_id_greater_than_0_is_not_supplied_async()
        {
            var command = new DeleteProductCommand(0, Mock.Of<IProductDataProxy>(), Mock.Of<IInventoryItemService>(), Mock.Of<IOrderDataProxy>(), Mock.Of<ITransactionContext>());
            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task Execution_should_fail_when_product_is_associated_with_orders_items_async()
        {
            var orderProxy = new Mock<IOrderDataProxy>();
            orderProxy.Setup(proxy => proxy.GetByProductAsync(1)).Returns(Task.FromResult<IEnumerable<Order>>(new[] { new Order() }));

            var command = new DeleteProductCommand(1, Mock.Of<IProductDataProxy>(),
                                                      Mock.Of<IInventoryItemService>(),
                                                      orderProxy.Object, 
                                                      Mock.Of<ITransactionContext>());
            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task Execution_deletes_product_and_associated_inventory_items_async()
        {
            var orderProxy = new Mock<IOrderDataProxy>();
            orderProxy.Setup(proxy => proxy.GetByProductAsync(1)).Returns(Task.FromResult<IEnumerable<Order>>(Enumerable.Empty<Order>()));

            var product = new Product() { ProductID = 1 };
            var productDataProxy = new ProductRepository();
            productDataProxy.Clear();
            productDataProxy.Insert(product);

            var inventoryDataProxy = new InventoryItemRepository();
            inventoryDataProxy.Clear();
            inventoryDataProxy.Insert(new InventoryItem() { ProductID = 1 });

            var command = new DeleteProductCommand(1, productDataProxy,
                                                      new InventoryItemService(inventoryDataProxy), 
                                                      orderProxy.Object,
                                                      new TransactionContextStub());
            var result = await command.ExecuteAsync();
            result.Success.ShouldBe(true);
            result.Errors.ShouldBe(null);
            productDataProxy.GetAll().Count().ShouldBe(0);
            inventoryDataProxy.GetAll().Count().ShouldBe(0);
        }
    }
}
