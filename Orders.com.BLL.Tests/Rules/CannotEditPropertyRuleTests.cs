using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders.com.BLL.Rules;
using Shouldly;

namespace Orders.com.BLL.Tests.Rules
{
    [TestClass]
    public class CannotEditPropertyRuleTests
    {
        [TestMethod]
        public void Is_always_valid()
        {
            var rule = new CannotEditPropertyRule("SomeProperty");
            rule.Validate().IsValid.ShouldBe(false);
        }
    }
}
