using System;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Attributes
{
    /// <summary>
    /// Validates that non-zero values are supplied for int, decimal or non-default values for dates and guids
    /// </summary>
    public class PeasyRequiredAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = $"The {validationContext.DisplayName} field is required.";
            var validationResult = new ValidationResult(errorMessage, new string[] { validationContext.DisplayName });

            switch (value)
            {
                case null:
                    return validationResult;
                case string _ when string.IsNullOrWhiteSpace(value.ToString()):
                    return validationResult;
                case Guid _ when Guid.Parse(value.ToString()) == Guid.Empty:
                    return validationResult;
            }

            var incoming = value.ToString();

            if (decimal.TryParse(incoming, out var val) && val == 0)
            {
                return validationResult;
            }

            if (DateTime.TryParse(incoming, out var date) && date == DateTime.MinValue)
            {
                return validationResult;
            }

            return ValidationResult.Success;
        }
    }
}
