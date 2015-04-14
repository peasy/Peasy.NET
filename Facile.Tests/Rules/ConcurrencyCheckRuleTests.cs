using Facile.Rules;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Facile.Tests.Rules
{
    [Trait("Rules", "ConcurrecyCheckRule")]
    public class ConcurrencyCheckRuleTests
    {
        [Fact]
        public void ConcurrencyCheckRuleReturnsTrue()
        {
            var version = new byte[ ] { 0, 1, 2, 3, 4, 5, 6 };
            var obj1 = new Mock<DomainBase>().Object;
            obj1.Version = version;
            var obj2 = new Mock<DomainBase>().Object;
            obj2.Version = version;
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            Assert.True(rule.Validate().IsValid);
        }

        [Fact]
        public void ConcurrencyCheckRuleReturnsFalse()
        {
            var version = new byte[] { 0, 1, 2, 3, 4, 5, 6 };
            var obj1 = new Mock<DomainBase>().Object;
            obj1.Version = version;
            var obj2 = new Mock<DomainBase>().Object;
            obj2.Version = new byte[] { 1, 2, 3, 4, 5, 6 };
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            Assert.False(rule.Validate().IsValid);
        }

        [Fact]
        public void ConcurrencyCheckRuleSetsErrorMessageOnInvalid()
        {
            var version = new byte[] { 0, 1, 2, 3, 4, 5, 6 };
            var obj1 = new Mock<DomainBase>().Object;
            obj1.Version = version;
            var obj2 = new Mock<DomainBase>().Object;
            obj2.Version = new byte[] { 1, 2, 3, 4, 5, 6 };
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            Assert.NotEqual("", rule.Validate().ErrorMessage);
        }
    }
}
