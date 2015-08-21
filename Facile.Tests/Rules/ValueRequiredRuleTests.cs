using Facile.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Facile.Tests.Rules
{
    public class ValueRequiredRuleTests
    {
        [Fact]
        public void ReturnsTrueWhenIntegerGreaterThanZeroSupplied()
        {
            var rule = new ValueRequiredRule(1, "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void ReturnsFalseWhenIntegerLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0, "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void SetsErrorMessageWhenIntegerLessThanOneSupplied()
        {
            var rule = new ValueRequiredRule(0, "id").Validate();
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [Fact]
        public void ReturnsTrueWhenStringNonEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("123", "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void ReturnsFalseWhenStringEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("", "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void SetsErrorMessageWhenStringEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule("", "id").Validate();
            rule.ErrorMessage.ShouldBe("id must be supplied");
        }

        [Fact]
        public void ReturnsTrueWhenGuidNonEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(Guid.NewGuid(), "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void ReturnsFalseWhenGuidEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void SetsErrorMessageWhenGuidEmptyIsSupplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Validate();
            rule.ErrorMessage.ShouldBe("id must be supplied");
        }
    }
}
