using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Peasy;

namespace Orders.com.BLL.Tests.Rules
{
    [TestClass]
    public class CanDeleteProductRuleTests
    {
        [TestMethod]
        public void Is_valid_when_no_associated_orders_exist()
        {
            var productID = 1;
            var orderDataProxy = new Mock<IOrderDataProxy>();
            orderDataProxy.Setup(p => p.GetByProduct(productID)).Returns(Enumerable.Empty<Order>());
            var orderService = new OrderService(orderDataProxy.Object, Mock.Of<IOrderItemService>(), Mock.Of<ITransactionContext>());
            var rule = new CanDeleteProductRule(productID, orderService);
            rule.Validate().IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public void Is_invalid_when_associated_orders_exist()
        {
            var productID = 1;
            var orderDataProxy = new Mock<IOrderDataProxy>();
            orderDataProxy.Setup(p => p.GetByProduct(productID)).Returns(Enumerable.OfType<Order>(new[] { new Order() }));
            var orderService = new OrderService(orderDataProxy.Object, Mock.Of<IOrderItemService>(), Mock.Of<ITransactionContext>());
            var rule = new CanDeleteProductRule(productID, orderService);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public async Task Is_valid_when_no_associated_orders_exist_async()
        {
            var productID = 1;
            var orderDataProxy = new Mock<IOrderDataProxy>();
            orderDataProxy.Setup(p => p.GetByProductAsync(productID))
                          .Returns(Task.FromResult(Enumerable.Empty<Order>()));
            var orderService = new OrderService(orderDataProxy.Object, Mock.Of<IOrderItemService>(), Mock.Of<ITransactionContext>());
            var rule = new CanDeleteProductRule(productID, orderService);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public async Task Is_invalid_when_associated_orders_exist_async()
        {
            var productID = 1;
            var orderDataProxy = new Mock<IOrderDataProxy>();
            orderDataProxy.Setup(p => p.GetByProductAsync(productID))
                          .Returns(Task.FromResult(Enumerable.OfType<Order>(new[] { new Order() })));
            var orderService = new OrderService(orderDataProxy.Object, Mock.Of<IOrderItemService>(), Mock.Of<ITransactionContext>());
            var rule = new CanDeleteProductRule(productID, orderService);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }
    }
}
