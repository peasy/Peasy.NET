using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Commands;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Orders.com.DAL.Mock;
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
        [TestMethod]
        public void Execution_should_fail_when_all_required_params_are_not_set()
        {
            var product = new Product();
            var command = CreateCommand(product);
            var result = command.Execute();
            result.Success.ShouldBe(false);
            result.Errors.Count().ShouldBe(3);
        }

        //[TestMethod]
        //public void Execution_should_fail_when_all_required_params_are_not_set()
        //{
        //    var product = new Product();
        //    var command = new CreateProductCommand(product,
        //                                           Mock.Of<IProductDataProxy>(),
        //                                           Mock.Of<IInventoryItemService>(),
        //                                           new MockTransactionContext());

        //    var result = command.Execute();
        //    result.Success.ShouldBe(false);
        //    result.Errors.Count().ShouldBe(3);
        //}

        [TestMethod]
        public void Execution_should_succeed_when_all_required_params_are_set()
        {
            var product = CreateValidProduct();
            var command = CreateCommand(product);
            var result = command.Execute();
            result.Success.ShouldBe(true);
            result.Errors.ShouldBeNull();
        }

        //[TestMethod]
        //public void Execution_should_succeed_when_all_required_params_are_set()
        //{
        //    var product = CreateValidProduct();

        //    var productDataProxy = new Mock<IProductDataProxy>();
        //    productDataProxy.Setup(p => p.Insert(product))
        //             .Returns(product)
        //             .Callback<Product>(p => product.ID = 10);

        //    var inventoryDataProxy = new Mock<IInventoryItemDataProxy>();
        //    inventoryDataProxy.Setup(dp => dp.Insert(It.IsAny<InventoryItem>()));

        //    var command = new CreateProductCommand(product,
        //                                           productDataProxy.Object,
        //                                           new InventoryItemService(inventoryDataProxy.Object),
        //                                           new MockTransactionContext());
        //    var result = command.Execute();
        //    result.Success.ShouldBe(true);
        //}


        //[TestMethod]
        //public void Product_and_inventory_item_should_be_created()
        //{
        //    var product = CreateValidProduct();
        //    var productDataProxy = new ProductRepository();
        //    var inventoryDataProxy = new InventoryItemRepository();
        //    var inventoryService = new InventoryItemService(inventoryDataProxy);
        //    var command = new CreateProductCommand(product, productDataProxy, inventoryService, new MockTransactionContext());

        //    var newProduct = command.Execute().Value;

        //    productDataProxy.GetByID(newProduct.ID).ShouldNotBeNull();
        //    inventoryDataProxy.GetByProduct(newProduct.ID).ShouldNotBeNull(); 
        //}

        [TestMethod]
        public void Product_and_inventory_item_should_be_created()
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
                                                   new MockTransactionContext());
            command.Execute();
            //productDataProxy.Verify(p => p.Insert(It.Is<Product>(pr => pr.ProductID == product.ProductID)),Times.Once());
            inventoryDataProxy.Verify(p => p.Insert(It.Is<InventoryItem>(i => i.ProductID == 10)), Times.Once());
        }

        private ICommand<Product> CreateCommand(Product product)
        {
            var productDataProxy = new ProductRepository();
            var inventoryDataProxy = new InventoryItemRepository();
            var inventoryService = new InventoryItemService(inventoryDataProxy);
            return new CreateProductCommand(product, productDataProxy, inventoryService, new MockTransactionContext());
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

        private ICommand<Product> CreateCommandII(Product product, Mock<IProductDataProxy> productDataProxyMock, Mock<IInventoryItemDataProxy> inventoryDataProxyMock)
        {
            productDataProxyMock.Setup(p => p.Insert(product))
                                .Callback<Product>(p => product.ID = 10)
                                .Returns(product);

            inventoryDataProxyMock.Setup(dp => dp.Insert(It.IsAny<InventoryItem>()));

            return new CreateProductCommand(product,
                                            productDataProxyMock.Object,
                                            new InventoryItemService(inventoryDataProxyMock.Object),
                                            new MockTransactionContext());
        }
    }
}
