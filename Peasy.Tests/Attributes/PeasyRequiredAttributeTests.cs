using Peasy.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Tests.Attributes
{
    public class MockClass<T>
    {
        public T Value { get; set; }
    }

    public class MockClassWithDisplayAttribute<T>
    {
        [Display(Name="ID")]
        public T Value { get; set; }
    }

    [TestClass]
    public class PeasyRequiredAttributeTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ThrowsExceptionWhenValueIsIntAndContainsZero()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new MockClass<int>();
            var context = new ValidationContext(foo);
            attr.Validate(0, context);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ThrowsExceptionWhenValueIsDecimalAndContainsZero()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new MockClass<decimal>();
            var context = new ValidationContext(foo);
            attr.Validate(0, context);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ThrowsExceptionWhenValueIsDateTimeAndContainsDefaultDate()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new MockClass<DateTime>();
            var context = new ValidationContext(foo);
            attr.Validate(0, context);
        }

        [TestMethod]
        public void SetsErrorMessageAndDisplaysMemberName()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new MockClass<int>();
            var context = new ValidationContext(foo);
            context.MemberName = "Value";
            var result = attr.GetValidationResult(0, context);
            result.ErrorMessage.ShouldBe("The Value field is required.");
        }

        [TestMethod]
        public void SetsErrorMessageAndDisplaysAppliedDisplayAttribute()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new MockClassWithDisplayAttribute<int>();
            var context = new ValidationContext(foo);
            context.MemberName = "Value";
            var result = attr.GetValidationResult(0, context);
            result.ErrorMessage.ShouldBe("The ID field is required.");
        }
    }
}
