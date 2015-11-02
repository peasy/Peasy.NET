using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Core
{
    public static class IDomainObjectExtensions
    {
        public static IEnumerable<ValidationResult> GetValidationErrors<T>(this T domainObject) where T : IDomainObject
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(domainObject);
            Validator.TryValidateObject(domainObject, context, validationResults, true);
            return validationResults;
        }
    }
}
