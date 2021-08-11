using Peasy.Attributes;
using Shouldly;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Peasy.Tests.Attributes
{
    public class Stub<T>
    {
        public T Value { get; set; }
    }

    public class StubWithDisplayAttribute<T>
    {
        [Display(Name="ID")]
        public T Value { get; set; }
    }

    public class PeasyRequiredAttributeTests
    {
        [Fact]
        public void Throws_Exception_When_Value_Is_Int_And_Contains_Zero()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new Stub<int>();
            var context = new ValidationContext(foo);

            Should.Throw<ValidationException>(() => attr.Validate(0, context));
        }

        [Fact]
        public void Throws_Exception_When_Value_Is_Long_And_Contains_Zero()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new Stub<long>();
            var context = new ValidationContext(foo);

            Should.Throw<ValidationException>(() => attr.Validate(0, context));
        }

        [Fact]
        public void Throws_Exception_When_Value_Is_Decimal_And_Contains_Zero()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new Stub<decimal>();
            var context = new ValidationContext(foo);

            Should.Throw<ValidationException>(() => attr.Validate(0, context));
        }

        [Fact]
        public void Throws_Exception_When_Value_Is_DateTime_And_Contains_Default_Date()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new Stub<DateTime>();
            var context = new ValidationContext(foo);

            Should.Throw<ValidationException>(() => attr.Validate(0, context));
        }

        [Fact]
        public void Throws_Exception_When_Value_Is_Guid_And_Contains_Guid_Empty()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new Stub<Guid>();
            var context = new ValidationContext(foo);

            Should.Throw<ValidationException>(() => attr.Validate(Guid.Empty, context));
        }

        [Fact]
        public void Throws_Exception_When_Value_Is_String_And_Contains_Empty_Value()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new Stub<string>();
            var context = new ValidationContext(foo);

            Should.Throw<ValidationException>(() => attr.Validate(string.Empty, context));
        }

        [Fact]
        public void Sets_ErrorMessage_And_Displays_Member_Name()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new Stub<int>();
            var context = new ValidationContext(foo) {MemberName = "Value"};
            var result = attr.GetValidationResult(0, context);
            result.ErrorMessage.ShouldBe("The Value field is required.");
        }

        [Fact]
        public void Sets_ErrorMessage_And_Displays_Applied_DisplayAttribute()
        {
            var attr = new PeasyRequiredAttribute();
            var foo = new StubWithDisplayAttribute<int>();
            var context = new ValidationContext(foo) {MemberName = "Value"};
            var result = attr.GetValidationResult(0, context);
            result.ErrorMessage.ShouldBe("The ID field is required.");
        }
    }
}