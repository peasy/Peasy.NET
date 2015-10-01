using System;
using Peasy.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Peasy.Tests.Rules
{
    [TestClass]
    public class ConcurrencyCheckRuleTests
    {
        [TestMethod]
        public void ConcurrencyCheckRuleReturnsTrue()
        {
            var obj1 = new ConcurrencyMock() { Version = "1" };
            var obj2 = new ConcurrencyMock() { Version = "1" };
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            rule.Validate().IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ConcurrencyCheckRuleReturnsFalse()
        {
            var obj1 = new ConcurrencyMock() { Version = "1" };
            var obj2 = new ConcurrencyMock() { Version = "2" };
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            rule.Validate().IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void ConcurrencyCheckRuleSetsErrorMessageOnInvalid()
        {
            var obj1 = new ConcurrencyMock() { Version = "1" };
            var obj2 = new ConcurrencyMock() { Version = "2" };
            var rule = new ConcurrencyCheckRule(obj1, obj2);
            rule.Validate().ErrorMessage.ShouldNotBeEmpty();
        }
    }

    public class ConcurrencyMock : IVersionContainer
    {
        public string Version { get; set; }
    }
}
