using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Facile.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<ValidationResult> GetValidationErrors<T>(this T domainObject)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(domainObject);
            Validator.TryValidateObject(domainObject, context, validationResults, true);
            return validationResults;
        }

        public static T CreateCopy<T>(this T domainObject)
        {
            //return Mapper.Map(domainObject, default(T));
            return domainObject;
            // TODO: Use DataContractSerializer here
        }

    }
}
