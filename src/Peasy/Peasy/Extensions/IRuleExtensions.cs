using System;
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

        public static Task<IEnumerable<ValidationResult>> GetValidationResultsAsync(this IRule businessRule)
        {
            return IRuleExtensions.GetValidationResultsAsync(businessRule.ToArray(), null);
        }

        public static IEnumerable<T> GetValidationResults<T>(this IRule businessRule) where T : ValidationResult
        {
            return GetValidationResults<T>(businessRule.ToArray(), null);
        }

        public static Task<IEnumerable<T>> GetValidationResultsAsync<T>(this IRule businessRule) where T : ValidationResult
        {
            return GetValidationResultsAsync<T>(businessRule.ToArray(), null);
        }

        public static IEnumerable<T> GetValidationResults<T>(this IEnumerable<IRule> businessRules) where T : ValidationResult
        {
            return GetValidationResults<T>(businessRules, null);
        }

        public static IEnumerable<T> GetValidationResults<T>(this IEnumerable<IRule> businessRules, string entityName) where T : ValidationResult
        {
            var rules = businessRules.Select(rule => rule.Validate())
                                     .Where(rule => !rule.IsValid)
                                     .Select(rule =>
                                     {
                                         return Activator.CreateInstance(typeof(T), rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }) as T;
                                     });
            return rules;
        }

        public static Task<IEnumerable<T>> GetValidationResultsAsync<T>(this IEnumerable<IRule> businessRules) where T : ValidationResult
        {
            return GetValidationResultsAsync<T>(businessRules, null);
        }

        public static async Task<IEnumerable<T>> GetValidationResultsAsync<T>(this IEnumerable<IRule> businessRules, string entityName) where T : ValidationResult
        {
            var rules = await Task.WhenAll(businessRules.Select(rule => rule.ValidateAsync()));

            return rules.Where(rule => !rule.IsValid)
                        .Select(rule =>
                        {
                            return Activator.CreateInstance(typeof(T), rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }) as T;
                        });
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
