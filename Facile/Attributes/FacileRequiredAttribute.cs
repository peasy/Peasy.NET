using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Attributes
{
    public class FacileRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = string.Format("The {0} field is required.", validationContext.DisplayName);
            var validationResult = new ValidationResult(errorMessage, new string[] { validationContext.DisplayName });
            if (value == null) return validationResult;

            if (value is string && string.IsNullOrWhiteSpace(value.ToString()))
            {
                return validationResult;
            }

            string incoming = value.ToString();

            decimal val = 0;
            if (Decimal.TryParse(incoming, out val))
            {
                if (val == 0) return validationResult;
            }

            DateTime date = DateTime.MinValue;
            if (DateTime.TryParse(incoming, out date))
            {
                if (date == DateTime.MinValue) return validationResult;
            }

            return ValidationResult.Success;
        }
    }
}
