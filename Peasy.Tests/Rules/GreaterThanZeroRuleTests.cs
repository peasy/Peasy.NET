using Peasy.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Peasy.Tests.Rules
{
    [TestClass]
    class GreaterThanZeroRuleTests
    {
        [TestMethod]
        public void ReturnsTrue()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(1, "foo");
            greaterThanZeroRule.Validate().IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ReturnsFalse()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(0, "foo");
            greaterThanZeroRule.Validate().IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void SetsErrorMessageOnInvalid()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(0, "the supplied value must be greater than 0");
            greaterThanZeroRule.Validate().ErrorMessage.ShouldBe("the supplied value must be greater than 0");
        }
    }
}
