using Peasy.Rules;
using Shouldly;
using Xunit;

namespace Peasy.Tests.Rules
{
    public class GreaterThanZeroRuleTests
    {
        [Fact]
        public void Returns_True()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(1, "foo");
            greaterThanZeroRule.Validate().IsValid.ShouldBe(true);
        }

        [Fact]
        public void Returns_False()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(0, "foo");
            greaterThanZeroRule.Validate().IsValid.ShouldBe(false);
        }

        [Fact]
        public void Sets_ErrorMessage_On_Invalid()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(0, "the supplied value must be greater than 0");
            greaterThanZeroRule.Validate().ErrorMessage.ShouldBe("the supplied value must be greater than 0");
        }
    }
}
