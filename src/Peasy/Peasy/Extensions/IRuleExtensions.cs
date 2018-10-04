using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    public static class IRuleExtensions
    {
        public static IEnumerable<ValidationResult> GetValidationResults(this IEnumerable<IRule> businessRules, string entityName)
        {
            var rules = businessRules.Select(rule => rule.Validate())
                                     .Where(rule => !rule.IsValid)
                                     .Select(rule => new ValidationResult(rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }));
            return rules;
        }

        public static IEnumerable<ValidationResult> GetValidationResults(this IEnumerable<IRule> businessRules)
        {
            return IRuleExtensions.GetValidationResults(businessRules, null);
        }

        public static IEnumerable<ValidationResult> GetValidationResults(this IRule businessRules)
        {
            return IRuleExtensions.GetValidationResults(new [] { businessRules }, null);
        }

        public static async Task<IEnumerable<ValidationResult>> GetValidationResultsAsync(this IEnumerable<IRule> businessRules, string entityName)
        {
            var rules  = await Task.WhenAll(businessRules.Select(r => r.ValidateAsync()));
            return rules.Where(rule => !rule.IsValid)
                        .Select(rule => new ValidationResult(rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }));
        }

        public static Task<IEnumerable<ValidationResult>> GetValidationResultsAsync(this IEnumerable<IRule> businessRules)
        {
            return IRuleExtensions.GetValidationResultsAsync(businessRules, null);
        }

        public static Task<IEnumerable<ValidationResult>> GetValidationResultsAsync(this IRule businessRules)
        {
            return IRuleExtensions.GetValidationResultsAsync(new [] { businessRules }, null);
        }

        public static IRule IfAllValidThenValidate(this IEnumerable<IRule> r, params IRule[] rules)
        {
            return new ValidRule().IfValidThenValidate(r.ToArray()).IfValidThenValidate(rules);
        }

        public static IEnumerable<IRule> ToArray(this IRule rule)
        {
            return new[] { rule };
        }
    }

    internal class ValidRule: RuleBase
    {
    }
}
