using Facile.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Facile.Tests.Rules
{
    public class ValueRequiredRuleTests
    {
        [Fact]
        public void ReturnsTrueWhenIntegerGreaterThanZeroSupplied()
        {
            var rule = new ValueRequiredRule(1, "id").Validate();
            Assert.True(rule.IsValid);
        }

        [Fact]
        public void ReturnsFalseWhenIntegerLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0, "id").Validate();
            Assert.False(rule.IsValid);
        }

        [Fact]
        public void SetsErrorMessageWhenIntegerLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0, "id").Validate();
            Assert.Equal("id must be greater than 0", rule.ErrorMessage);
        }

        [Fact]
        public void ReturnsTrueWhenStringNonEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("123", "id").Validate();
            Assert.True(rule.IsValid);
        }

        [Fact]
        public void ReturnsFalseWhenStringEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("", "id").Validate();
            Assert.False(rule.IsValid);
        }

        [Fact]
        public void SetsErrorMessageWhenStringEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("", "id").Validate();
            Assert.Equal("id must be supplied", rule.ErrorMessage);
        }

        [Fact]
        public void ReturnsTrueWhenGuidNonEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(Guid.NewGuid(), "id").Validate();
            Assert.True(rule.IsValid);
        }

        [Fact]
        public void ReturnsFalseWhenGuidEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Validate();
            Assert.False(rule.IsValid);
        }

        [Fact]
        public void SetsErrorMessageWhenGuidEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Validate();
            Assert.Equal("id must be supplied", rule.ErrorMessage);
        }
    }
}
