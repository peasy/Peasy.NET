using System;
using System.Threading.Tasks;
using Peasy.Extensions;
using Peasy.Rules;
using Shouldly;
using Xunit;

namespace Peasy.Core.Tests.Extensions
{
    public class GenericExtensionsTests
    {
        [Fact]
        public void CreateValueRequiredRule_Returns_Composed_Rule_For_String()
        {
            string nameValue = "testing";
            var rule = nameValue.CreateValueRequiredRule("name");
            rule.ShouldBeOfType<ValueRequiredRule>();
        }

        [Fact]
        public void CreateValueRequiredRule_Returns_Composed_Rule_For_Long()
        {
            long salaryValue = 123456789;
            var rule = salaryValue.CreateValueRequiredRule("salary");
            rule.ShouldBeOfType<ValueRequiredRule>();
        }

        [Fact]
        public void CreateValueRequiredRule_Returns_Composed_Rule_For_Decimal()
        {
            decimal salaryValue = 1234567.89M;
            var rule = salaryValue.CreateValueRequiredRule("salary");
            rule.ShouldBeOfType<ValueRequiredRule>();
        }

        [Fact]
        public void CreateValueRequiredRule_Returns_Composed_Rule_For_Double()
        {
            double salaryValue = 1234567.89;
            var rule = salaryValue.CreateValueRequiredRule("salary");
            rule.ShouldBeOfType<ValueRequiredRule>();
        }

        [Fact]
        public void CreateValueRequiredRule_Returns_Composed_Rule_For_Integer()
        {
            int ageValue = 99;
            var rule = ageValue.CreateValueRequiredRule("ageValue");
            rule.ShouldBeOfType<ValueRequiredRule>();
        }

        [Fact]
        public void CreateValueRequiredRule_Returns_Composed_Rule_For_Guid()
        {
            Guid idValue = Guid.NewGuid();
            var rule = idValue.CreateValueRequiredRule("idValue");
            rule.ShouldBeOfType<ValueRequiredRule>();
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_String_With_Null_Value()
        {
            Stub stub = new Stub();
            var rule = stub.StringValue.CreateValueRequiredRule("string value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeFalse();
            rule.ErrorMessage.ShouldBe("string value must be supplied");
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Int_Without_Value()
        {
            Stub stub = new Stub();
            var rule = stub.IntValue.CreateValueRequiredRule("int value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeFalse();
            rule.ErrorMessage.ShouldBe("int value must be greater than 0");
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Int_With_Value()
        {
            Stub stub = new Stub { IntValue = 124123467 };
            var rule = stub.IntValue.CreateValueRequiredRule("int value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Long_Without_Value()
        {
            Stub stub = new Stub();
            var rule = stub.LongValue.CreateValueRequiredRule("long value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeFalse();
            rule.ErrorMessage.ShouldBe("long value must be greater than 0");
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Long_With_Value()
        {
            Stub stub = new Stub { LongValue = 12412345678234567 };
            var rule = stub.LongValue.CreateValueRequiredRule("long value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Double_Without_Value()
        {
            Stub stub = new Stub();
            var rule = stub.DoubleValue.CreateValueRequiredRule("double value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeFalse();
            rule.ErrorMessage.ShouldBe("double value must be greater than 0");
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Double_With_Value()
        {
            Stub stub = new Stub { DoubleValue = 124.45 };
            var rule = stub.DoubleValue.CreateValueRequiredRule("double value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Decimal_Without_Value()
        {
            Stub stub = new Stub();
            var rule = stub.DecimalValue.CreateValueRequiredRule("decimal value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeFalse();
            rule.ErrorMessage.ShouldBe("decimal value must be greater than 0");
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Decimal_With_Value()
        {
            Stub stub = new Stub { DecimalValue = 124.45M };
            var rule = stub.DecimalValue.CreateValueRequiredRule("decimal value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Guid_Without_Value()
        {
            Stub stub = new Stub();
            var rule = stub.GuidValue.CreateValueRequiredRule("guid value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeFalse();
            rule.ErrorMessage.ShouldBe("A valid UUID for guid value must be supplied");
        }

        [Fact]
        public async Task CreateValueRequiredRule_Returns_Composed_Rule_For_Nullable_Guid_With_Value()
        {
            Stub stub = new Stub { GuidValue = Guid.NewGuid() };
            var rule = stub.GuidValue.CreateValueRequiredRule("guid value");

            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void CreateValueRequiredRule_Returns_Null_For_Unsupported_Type()
        {
            Stub stub = new Stub();
            var rule = stub.CreateValueRequiredRule("stub");
            rule.ShouldBeNull();
        }

        public class Stub
        {
            public string StringValue { get; set; }
            public int? IntValue { get; set; }
            public long? LongValue { get; set; }
            public decimal? DecimalValue { get; set; }
            public double? DoubleValue { get; set; }
            public Guid? GuidValue { get; set; }
        }
    }
}
