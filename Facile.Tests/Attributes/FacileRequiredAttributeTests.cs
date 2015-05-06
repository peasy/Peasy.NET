using Facile.Attributes;
using Facile.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Facile.Tests.Attributes
{
    public class TestClass<T>
    {
        public T Value { get; set; }
    }

    public class TestClassWithDisplayAttribute<T>
    {
        [Display(Name="ID")]
        public T Value { get; set; }
    }

    public class FacileRequiredAttributeTests
    {
        [Fact]
        public void ThrowsExceptionWhenValueIsIntAndContainsZero()
        {
            var attr = new FacileRequiredAttribute();
            var foo = new TestClass<int>();
            var context = new ValidationContext(foo);
            Assert.Throws<ValidationException>(() => attr.Validate(0, context));
        }

        [Fact]
        public void ThrowsExceptionWhenValueIsDecimalAndContainsZero()
        {
            var attr = new FacileRequiredAttribute();
            var foo = new TestClass<decimal>();
            var context = new ValidationContext(foo);
            Assert.Throws<ValidationException>(() => attr.Validate(0, context));
        }

        [Fact]
        public void ThrowsExceptionWhenValueIsDateTimeAndContainsDefaultDate()
        {
            var attr = new FacileRequiredAttribute();
            var foo = new TestClass<DateTime>();
            var context = new ValidationContext(foo);
            Assert.Throws<ValidationException>(() => attr.Validate(DateTime.MinValue, context));
        }

        [Fact]
        public void SetsErrorMessageAndDisplaysMemberName()
        {
            var attr = new FacileRequiredAttribute();
            var foo = new TestClass<int>();
            var context = new ValidationContext(foo);
            context.MemberName = "Value";
            var result = attr.GetValidationResult(0, context);
            Assert.Equal("The Value field is required.", result.ErrorMessage);
        }

        [Fact]
        public void SetsErrorMessageAndDisplaysAppliedDisplayAttribute()
        {
            var attr = new FacileRequiredAttribute();
            var foo = new TestClassWithDisplayAttribute<int>();
            var context = new ValidationContext(foo);
            context.MemberName = "Value";
            var result = attr.GetValidationResult(0, context);
            Assert.Equal("The ID field is required.", result.ErrorMessage);
        }
    }
}
