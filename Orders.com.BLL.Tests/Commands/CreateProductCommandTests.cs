using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Commands;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Orders.com.DAL.InMemory;
using Peasy;
using Peasy.Core;
using Peasy.Rules;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.BLL.Tests.Commands
{
    [TestClass]
    public class CreateProductCommandTests
    {
        #region State-Based Tests

        [TestMethod]
        public void Execution_should_fail_when_all_required_params_are_not_set()
        {
            var product = new Product();
            var command = CreateCommand(product);
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(3);
        }

        [TestMethod]
        public void Execution_should_succeed_when_all_required_params_are_set()
        {
            var product = CreateValidProduct();
            var command = CreateCommand(product);
            var result = command.Execute();
            result.Success.ShouldBe(true);
            result.Errors.ShouldBeNull();
        }

        [TestMethod]
        public void Product_and_inventory_item_should_be_created()
        {
            var product = CreateValidProduct();
            var productDataProxy = new ProductRepository();
            var inventoryDataProxy = new InventoryItemRepository();
            var inventoryService = new InventoryItemService(inventoryDataProxy);
            var command = new CreateProductCommand(product, productDataProxy, inventoryService, new TransactionContextStub());

            var newProduct = command.Execute().Value;

            productDataProxy.GetByID(newProduct.ID).ShouldNotBeNull();
            inventoryDataProxy.GetByProduct(newProduct.ID).ShouldNotBeNull();
        }

        #endregion

        #region Behavior-Based Tests

        [TestMethod]
        public void Execution_should_fail_when_all_required_params_are_not_set_behavior_based()
        {
            var product = new Product();
            var command = new CreateProductCommand(product,
                                                   Mock.Of<IProductDataProxy>(),
                                                   Mock.Of<IInventoryItemService>(),
                                                   new TransactionContextStub());

            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(3);
        }

        [TestMethod]
        public void Execution_should_succeed_when_all_required_params_are_set_behavior_based()
        {
            var product = CreateValidProduct();

            var productDataProxy = new Mock<IProductDataProxy>();
            productDataProxy.Setup(p => p.Insert(product))
                     .Returns(product)
                     .Callback<Product>(p => product.ID = 10);

            var inventoryDataProxy = new Mock<IInventoryItemDataProxy>();
            inventoryDataProxy.Setup(dp => dp.Insert(It.IsAny<InventoryItem>()));

            var command = new CreateProductCommand(product,
                                                   productDataProxy.Object,
                                                   new InventoryItemService(inventoryDataProxy.Object),
                                                   new TransactionContextStub());
            var result = command.Execute();
            result.Success.ShouldBe(true);
        }

        [TestMethod]
        public void Product_and_inventory_item_should_be_created_behavior_based()
        {
            var product = CreateValidProduct();

            var productDataProxy = new Mock<IProductDataProxy>();
            productDataProxy.Setup(p => p.Insert(product))
                            .Callback<Product>(p => product.ID = 10)
                            .Returns(product);

            var inventoryDataProxy = new Mock<IInventoryItemDataProxy>();
            inventoryDataProxy.Setup(dp => dp.Insert(It.IsAny<InventoryItem>()));

            var command = new CreateProductCommand(product,
                                                   productDataProxy.Object,
                                                   new InventoryItemService(inventoryDataProxy.Object),
                                                   new TransactionContextStub());
            command.Execute();
            //productDataProxy.Verify(p => p.Insert(It.Is<Product>(pr => pr.ProductID == product.ProductID)),Times.Once());
            inventoryDataProxy.Verify(p => p.Insert(It.Is<InventoryItem>(i => i.ProductID == 10)), Times.Once());
        }

        #endregion

        private ICommand<Product> CreateCommand(Product product)
        {
            var productDataProxy = new ProductRepository();
            var inventoryDataProxy = new InventoryItemRepository();
            var inventoryService = new InventoryItemService(inventoryDataProxy);
            return new CreateProductCommand(product, productDataProxy, inventoryService, new TransactionContextStub());
        }

        private Product CreateValidProduct()
        {
            return new Product
            {
                Name = "Guitar",
                CategoryID = 1,
                Price = 2000M
            };
        }
    }
}
