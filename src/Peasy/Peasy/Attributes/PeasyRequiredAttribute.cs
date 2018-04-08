using System;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Attributes
{
    /// <summary>
    /// Validates that non-zero values are supplied for int, decimal or non-default values for dates
    /// </summary>
    public class PeasyRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = $"The {validationContext.DisplayName} field is required.";
            var validationResult = new ValidationResult(errorMessage, new string[] { validationContext.DisplayName });

            if (value == null) return validationResult;

            if (value is string && string.IsNullOrWhiteSpace(value.ToString()))
            {
                return validationResult;
            }

            if (value is Guid && Guid.Parse(value.ToString()) == Guid.Empty)
            {
                return validationResult;
            }

            var incoming = value.ToString();

            decimal val = 0;
            if (Decimal.TryParse(incoming, out val) && val == 0)
            {
                return validationResult;
            }

            var date = DateTime.MinValue;
            if (DateTime.TryParse(incoming, out date) && date == DateTime.MinValue)
            {
                return validationResult;
            }

            return ValidationResult.Success;
        }
    }
}
