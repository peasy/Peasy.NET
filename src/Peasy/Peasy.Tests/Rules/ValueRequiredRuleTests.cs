using Peasy.Rules;
using Shouldly;
using System;
using Xunit;

namespace Peasy.Tests.Rules
{
    public class ValueRequiredRuleTests
    {
        [Fact]
        public void Returns_True_When_Integer_Greater_Than_Zero_Supplied()
        {
            var rule = new ValueRequiredRule(1, "id").Execute();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void Returns_False_When_Integer_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0, "id").Execute();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Sets_ErrorMessage_And_Association_When_Integer_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0, "id").Execute();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [Fact]
        public void Returns_True_When_Long_Greater_Than_Zero_Supplied()
        {
            var rule = new ValueRequiredRule(1L, "id").Execute();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void Returns_False_When_Long_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0L, "id").Execute();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Sets_ErrorMessage_And_Association_When_Long_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0L, "id").Execute();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [Fact]
        public void Returns_True_When_Decimal_Greater_Than_Zero_Supplied()
        {
            var rule = new ValueRequiredRule(1M, "id").Execute();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void Returns_False_When_Decimal_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0M, "id").Execute();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Sets_ErrorMessage_And_Association_When_Decimal_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0M, "id").Execute();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [Fact]
        public void Returns_True_When_String_Non_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule("123", "id").Execute();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void Returns_False_When_String_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule("", "id").Execute();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Sets_ErrorMessage_And_Association_When_String_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule("", "id").Execute();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be supplied");
        }

        [Fact]
        public void Returns_True_When_Guid_Non_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule(Guid.NewGuid(), "id").Execute();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void Returns_False_When_Guid_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Execute();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Returns_False_When_Null_Is_Supplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Execute();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Sets_ErrorMessage_And_Association_When_Guid_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Execute();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("A valid UUID for id must be supplied");
        }

        [Fact]
        public void Sets_ErrorMessage_And_Association_When_Null_Is_Supplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Execute();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("A valid UUID for id must be supplied");
        }
    }
}
