using Peasy.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Peasy.Tests.Rules
{
    [TestClass]
    class GreaterThanZeroRuleTests
    {
        [TestMethod]
        public void Returns_True()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(1, "foo");
            greaterThanZeroRule.Validate().IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void Returns_False()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(0, "foo");
            greaterThanZeroRule.Validate().IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void Sets_ErrorMessage_On_Invalid()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(0, "the supplied value must be greater than 0");
            greaterThanZeroRule.Validate().ErrorMessages.ShouldContainKeyAndValue(null, "the supplied value must be greater than 0");
        }
    }
}
