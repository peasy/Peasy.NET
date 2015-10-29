using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;

namespace Orders.com.BLL.Tests.Rules
{
    [TestClass]
    public class CanDeleteCategoryRuleTests
    {
        [TestMethod]
        public void Is_valid_when_no_associated_products_exist()
        {
            var categoryID = 1;
            var proxyMock = new Mock<IProductDataProxy>();
            proxyMock.Setup(proxy => proxy.GetByCategory(categoryID))
                     .Returns(Enumerable.Empty<Product>());
            var rule = new CanDeleteCategoryRule(categoryID, proxyMock.Object);
            rule.Validate().IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public void Is_invalid_when_associated_products_exist()
        {
            var categoryID = 1;
            var proxyMock = new Mock<IProductDataProxy>();
            proxyMock.Setup(proxy => proxy.GetByCategory(categoryID))
                     .Returns(new[] { new Product() });
            var rule = new CanDeleteCategoryRule(categoryID, proxyMock.Object);
            rule.Validate().IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }

        [TestMethod]
        public async Task Is_valid_when_no_associated_products_exist_async()
        {
            var categoryID = 1;
            var proxyMock = new Mock<IProductDataProxy>();
            proxyMock.Setup(proxy => proxy.GetByCategoryAsync(categoryID))
                     .Returns(Task.FromResult(Enumerable.Empty<Product>()));
            var rule = new CanDeleteCategoryRule(categoryID, proxyMock.Object);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(true);
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public async Task Is_invalid_when_associated_products_exist_async()
        {
            var categoryID = 1;
            var proxyMock = new Mock<IProductDataProxy>();
            proxyMock.Setup(proxy => proxy.GetByCategoryAsync(categoryID))
                     .Returns(Task.FromResult((new[] { new Product() }).AsEnumerable<Product>()));
            var rule = new CanDeleteCategoryRule(categoryID, proxyMock.Object);
            await rule.ValidateAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldNotBe(null);
        }
    }
}
