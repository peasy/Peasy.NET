using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    public interface IAuthorizationRule {}
    public class RuleA : RuleBase, IAuthorizationRule
    {
    }
    public class RuleB : RuleBase
    {
    }
    public class RuleC : RuleBase, IAuthorizationRule
    {
    }

    public class PeasyValidationResult : ValidationResult
    {
        public PeasyValidationResult(): base("") {}

        public PeasyValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        public PeasyValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }
    }

    public static class IRuleExtensions
    {
        public static IEnumerable<T> GetValidationResults<T>(this IEnumerable<IRule> businessRules) where T : ValidationResult, new()
        {
            return GetValidationResults<T>(businessRules, null);
        }

        public static IEnumerable<T> GetValidationResults<T>(this IEnumerable<IRule> businessRules, string entityName) where T : ValidationResult, new()
        {
            var rules = businessRules.Select(rule => rule.Validate())
                                     .Where(rule => !rule.IsValid)
                                     .Select(rule =>
                                     {
                                         return new PeasyValidationResult(rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }) as T;
                                        //  return Activator.CreateInstance(typeof(T), rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }) as T;
                                     });
            return rules;
        }

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
