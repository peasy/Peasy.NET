using Peasy.Rules;
using Shouldly;
using Xunit;

namespace Peasy.Tests.Rules
{
    public class ConcurrencyCheckRuleTests
    {
        [Fact]
        public void ConcurrencyCheckRule_Returns_True()
        {
            var obj1 = new ConcurrencyStub { Version = "1" };
            var obj2 = new ConcurrencyStub { Version = "1" };
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            rule.Validate().IsValid.ShouldBe(true);
        }

        [Fact]
        public void ConcurrencyCheckRule_Returns_False()
        {
            var obj1 = new ConcurrencyStub { Version = "1" };
            var obj2 = new ConcurrencyStub { Version = "2" };
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            rule.Validate().IsValid.ShouldBe(false);
        }

        [Fact]
        public void ConcurrencyCheckRule_Sets_ErrorMessage_On_Invalid()
        {
            var obj1 = new ConcurrencyStub { Version = "1" };
            var obj2 = new ConcurrencyStub { Version = "2" };
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            rule.Validate().ErrorMessage.ShouldNotBeEmpty();
        }
    }

    public class ConcurrencyStub : IVersionContainer
    {
        public string Version { get; set; }
    }
}
