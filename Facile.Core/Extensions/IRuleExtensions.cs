using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Core.Extensions
{
    public static class IRuleExtensions
    {
        public static IEnumerable<ValidationResult> GetBusinessRulesResults(this IEnumerable<IRule> businessRules, string entityName)
        {
            var invalidRules = businessRules.ToArray()
                                            .ForEach(rule => rule.Validate())
                                            .Where(rule => !rule.IsValid);

            foreach (var rule in invalidRules)
            {
                yield return new ValidationResult(rule.ErrorMessage, new string[] {  entityName });
            }
        }
    }
}
