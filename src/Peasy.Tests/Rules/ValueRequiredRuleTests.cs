using Peasy.Rules;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Tests.Rules
{
    public class ValueRequiredRuleTests
    {
        [Fact]
        public async Task Returns_True_When_Integer_Greater_Than_Zero_Supplied()
        {
            var rule = await new ValueRequiredRule(1, "id").ExecuteAsync();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public async Task Returns_False_When_Integer_Less_Than_One_Supplied()
        {
            var rule = await new ValueRequiredRule(0, "id").ExecuteAsync();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public async Task Sets_ErrorMessage_And_Association_When_Integer_Less_Than_One_Supplied()
        {
            var rule = await new ValueRequiredRule(0, "id").ExecuteAsync();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [Fact]
        public async Task Returns_True_When_Long_Greater_Than_Zero_Supplied()
        {
            var rule = await new ValueRequiredRule(1L, "id").ExecuteAsync();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public async Task Returns_False_When_Long_Less_Than_One_Supplied()
        {
            var rule = await new ValueRequiredRule(0L, "id").ExecuteAsync();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public async Task Sets_ErrorMessage_And_Association_When_Long_Less_Than_One_Supplied()
        {
            var rule = await new ValueRequiredRule(0L, "id").ExecuteAsync();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [Fact]
        public async Task Returns_True_When_Decimal_Greater_Than_Zero_Supplied()
        {
            var rule = await new ValueRequiredRule(1M, "id").ExecuteAsync();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public async Task Returns_False_When_Decimal_Less_Than_One_Supplied()
        {
            var rule = await new ValueRequiredRule(0M, "id").ExecuteAsync();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public async Task Sets_ErrorMessage_And_Association_When_Decimal_Less_Than_One_Supplied()
        {
            var rule = await new ValueRequiredRule(0M, "id").ExecuteAsync();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [Fact]
        public async Task Returns_True_When_Double_Greater_Than_Zero_Supplied()
        {
            double val = 1;
            var rule = await new ValueRequiredRule(val, "id").ExecuteAsync();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public async Task Returns_False_When_Double_Less_Than_One_Supplied()
        {
            double val = 0;
            var rule = await new ValueRequiredRule(val, "id").ExecuteAsync();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public async Task Sets_ErrorMessage_And_Association_When_Double_Less_Than_One_Supplied()
        {
            double val = 0;
            var rule = await new ValueRequiredRule(val, "id").ExecuteAsync();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be greater than 0");
        }

        [Fact]
        public async Task Returns_True_When_String_Non_Empty_Is_Supplied()
        {
            var rule = await new ValueRequiredRule("123", "id").ExecuteAsync();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public async Task Returns_False_When_String_Empty_Is_Supplied()
        {
            var rule = await new ValueRequiredRule("", "id").ExecuteAsync();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public async Task Sets_ErrorMessage_And_Association_When_String_Empty_Is_Supplied()
        {
            var rule = await new ValueRequiredRule("", "id").ExecuteAsync();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("id must be supplied");
        }

        [Fact]
        public async Task Returns_True_When_Guid_Non_Empty_Is_Supplied()
        {
            var rule = await new ValueRequiredRule(Guid.NewGuid(), "id").ExecuteAsync();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public async Task Returns_False_When_Guid_Empty_Is_Supplied()
        {
            var rule = await new ValueRequiredRule(new Guid(), "id").ExecuteAsync();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public async Task Returns_False_When_Null_Is_Supplied()
        {
            var rule = await new ValueRequiredRule(new Guid(), "id").ExecuteAsync();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public async Task Sets_ErrorMessage_And_Association_When_Guid_Empty_Is_Supplied()
        {
            var rule = await new ValueRequiredRule(new Guid(), "id").ExecuteAsync();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("A valid UUID for id must be supplied");
        }

        [Fact]
        public async Task Sets_ErrorMessage_And_Association_When_Null_Is_Supplied()
        {
            var rule = await new ValueRequiredRule(new Guid(), "id").ExecuteAsync();
            rule.Association.ShouldBe("id");
            rule.ErrorMessage.ShouldBe("A valid UUID for id must be supplied");
        }
    }
}
