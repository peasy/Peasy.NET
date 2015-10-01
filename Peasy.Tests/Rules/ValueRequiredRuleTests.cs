using Peasy.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace Peasy.Tests.Rules
{
    [TestClass]
    public class ValueRequiredRuleTests
    {
        [TestMethod]
        public void ReturnsTrueWhenIntegerGreaterThanZeroSupplied()
        {
            var rule = new ValueRequiredRule(1, "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ReturnsFalseWhenIntegerLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0, "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void SetsErrorMessageWhenIntegerLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0, "id").Validate();
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [TestMethod]
        public void ReturnsTrueWhenLongGreaterThanZeroSupplied()
        {
            var rule = new ValueRequiredRule(1L, "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ReturnsFalseWhenLongLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0L, "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void SetsErrorMessageWhenLongLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0L, "id").Validate();
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [TestMethod]
        public void ReturnsTrueWhenDecimalGreaterThanZeroSupplied()
        {
            var rule = new ValueRequiredRule(1M, "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ReturnsFalseWhenDecimalLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0M, "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void SetsErrorMessageWhenDecimalLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0M, "id").Validate();
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [TestMethod]
        public void ReturnsTrueWhenStringNonEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("123", "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ReturnsFalseWhenStringEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("", "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void SetsErrorMessageWhenStringEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("", "id").Validate();
            rule.ErrorMessage.ShouldBe("id must be supplied");
        }

        [TestMethod]
        public void ReturnsTrueWhenGuidNonEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(Guid.NewGuid(), "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ReturnsFalseWhenGuidEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void SetsErrorMessageWhenGuidEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Validate();
            rule.ErrorMessage.ShouldBe("id must be supplied");
        }
    }
}
