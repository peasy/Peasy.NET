using System;
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
        public void CreateValueRequiredRule_Returns_Null_For_Unsupported_Type()
        {
            Stub stub = new Stub();
            var rule = stub.CreateValueRequiredRule("stub");
            rule.ShouldBeNull();
        }

        class Stub
        {
        }
    }
}
