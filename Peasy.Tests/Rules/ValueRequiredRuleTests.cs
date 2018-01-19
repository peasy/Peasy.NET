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
        public void Returns_True_When_Integer_Greater_Than_Zero_Supplied()
        {
            var rule = new ValueRequiredRule(1, "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void Returns_False_When_Integer_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0, "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void Sets_ErrorMessage_When_Integer_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0, "id").Validate();
            rule.ErrorMessages.ShouldContainKeyAndValue(string.Empty, "id must be greater than 0");
        }

        [TestMethod]
        public void Returns_True_When_Long_Greater_Than_Zero_Supplied()
        {
            var rule = new ValueRequiredRule(1L, "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void Returns_False_When_Long_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0L, "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void Sets_ErrorMessage_When_Long_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0L, "id").Validate();
            rule.ErrorMessages.ShouldContainKeyAndValue(string.Empty, "id must be greater than 0");
        }

        [TestMethod]
        public void Returns_True_When_Decimal_Greater_Than_Zero_Supplied()
        {
            var rule = new ValueRequiredRule(1M, "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void Returns_False_When_Decimal_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0M, "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void Sets_ErrorMessage_When_Decimal_Less_Than_One_Supplied()
        {
            var rule = new ValueRequiredRule(0M, "id").Validate();
            rule.ErrorMessages.ShouldContainKeyAndValue(string.Empty, "id must be greater than 0");
        }

        [TestMethod]
        public void Returns_True_When_String_Non_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule("123", "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void Returns_False_When_String_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule("", "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void Sets_ErrorMessage_When_String_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule("", "id").Validate();
            rule.ErrorMessages.ShouldContainKeyAndValue(string.Empty, "id must be supplied");
        }

        [TestMethod]
        public void Returns_True_When_Guid_Non_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule(Guid.NewGuid(), "id").Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void Returns_False_When_Guid_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void Sets_ErrorMessage_When_Guid_Empty_Is_Supplied()
        {
            var rule = new ValueRequiredRule(new Guid(), "id").Validate();
            rule.ErrorMessages.ShouldContainKeyAndValue(string.Empty, "id must be supplied");
        }
    }
}
