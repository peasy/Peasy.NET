using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy
{
    public static class IDomainObjectExtensions
    {
        public static IEnumerable<ValidationResult> GetValidationErrors<T>(this T domainObject) where T : new()
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(domainObject);
            Validator.TryValidateObject(domainObject, context, validationResults, true);
            return validationResults;
        }
    }
}
