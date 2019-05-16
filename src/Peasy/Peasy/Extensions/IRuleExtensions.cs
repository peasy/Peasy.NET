using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    public static class IRuleExtensions
    {
        /// <summary>
        /// Synchronously invokes the rules
        /// </summary>
        /// <returns>
        /// List of validation results for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        /// <param name="entityName">The name to assign to the ValidationResult.MemberNames value</param>
        public static IEnumerable<ValidationResult> GetValidationResults(this IEnumerable<IRule> businessRules, string entityName)
        {
            var rules = businessRules.Select(rule => rule.Validate())
                                     .Where(rule => !rule.IsValid)
                                     .Select(rule => new ValidationResult(rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }));
            return rules;
        }

        /// <summary>
        /// Synchronously invokes the rules
        /// </summary>
        /// <returns>
        /// List of validation results for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        public static IEnumerable<ValidationResult> GetValidationResults(this IEnumerable<IRule> businessRules)
        {
            return IRuleExtensions.GetValidationResults(businessRules, null);
        }

        /// <summary>
        /// Synchronously invokes the rule
        /// </summary>
        /// <returns>
        /// List of validation results if the rule fails execution
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        public static IEnumerable<ValidationResult> GetValidationResults(this IRule businessRule)
        {
            return IRuleExtensions.GetValidationResults(new [] { businessRule }, null);
        }

        /// <summary>
        /// Synchronously invokes the rule
        /// </summary>
        /// <returns>
        /// List of validation results if the rule fails execution
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        /// <typeparam name="T">A type that inherits from System.ComponentModel.DataAnnotations.ValidationResult</typeparam>
        public static IEnumerable<T> GetValidationResults<T>(this IRule businessRule) where T : ValidationResult
        {
            return GetValidationResults<T>(businessRule.ToArray(), null);
        }

        /// <summary>
        /// Synchronously invokes the rules
        /// </summary>
        /// <returns>
        /// List of validation results for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        /// <typeparam name="T">A type that inherits from System.ComponentModel.DataAnnotations.ValidationResult</typeparam>
        public static IEnumerable<T> GetValidationResults<T>(this IEnumerable<IRule> businessRules) where T : ValidationResult
        {
            return GetValidationResults<T>(businessRules, null);
        }

        /// <summary>
        /// Synchronously invokes the rules
        /// </summary>
        /// <returns>
        /// List of validation results for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        /// <typeparam name="T">A type that inherits from System.ComponentModel.DataAnnotations.ValidationResult</typeparam>
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

        /// <summary>
        /// Asynchronously invokes the rules
        /// </summary>
        /// <returns>
        /// List of validation results for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        /// <param name="entityName">The name to assign to the ValidationResult.MemberNames value</param>
        public static async Task<IEnumerable<ValidationResult>> GetValidationResultsAsync(this IEnumerable<IRule> businessRules, string entityName)
        {
            var rules  = await Task.WhenAll(businessRules.Select(r => r.ValidateAsync()));
            return rules.Where(rule => !rule.IsValid)
                        .Select(rule => new ValidationResult(rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }));
        }

        /// <summary>
        /// Asynchronously invokes the rules
        /// </summary>
        /// <returns>
        /// List of validation results for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        public static Task<IEnumerable<ValidationResult>> GetValidationResultsAsync(this IEnumerable<IRule> businessRules)
        {
            return IRuleExtensions.GetValidationResultsAsync(businessRules, null);
        }

        /// <summary>
        /// Asynchronously invokes the rule
        /// </summary>
        /// <returns>
        /// List of validation results if the rule fails execution
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        public static Task<IEnumerable<ValidationResult>> GetValidationResultsAsync(this IRule businessRule)
        {
            return IRuleExtensions.GetValidationResultsAsync(businessRule.ToArray(), null);
        }

        /// <summary>
        /// Asynchronously invokes the rule
        /// </summary>
        /// <returns>
        /// List of validation results if the rule fails execution
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        /// <typeparam name="T">A type that inherits from System.ComponentModel.DataAnnotations.ValidationResult</typeparam>
        public static Task<IEnumerable<T>> GetValidationResultsAsync<T>(this IRule businessRule) where T : ValidationResult
        {
            return GetValidationResultsAsync<T>(businessRule.ToArray(), null);
        }

        /// <summary>
        /// Asynchronously invokes the rules
        /// </summary>
        /// <returns>
        /// List of validation results for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        /// <typeparam name="T">A type that inherits from System.ComponentModel.DataAnnotations.ValidationResult</typeparam>
        public static Task<IEnumerable<T>> GetValidationResultsAsync<T>(this IEnumerable<IRule> businessRules) where T : ValidationResult
        {
            return GetValidationResultsAsync<T>(businessRules, null);
        }

        /// <summary>
        /// Asynchronously invokes the rules
        /// </summary>
        /// <returns>
        /// List of validation results for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of rule to execute</param>
        /// <param name="entityName">The name to assign to the ValidationResult.MemberNames value</param>
        /// <typeparam name="T">A type that inherits from System.ComponentModel.DataAnnotations.ValidationResult</typeparam>
        public static async Task<IEnumerable<T>> GetValidationResultsAsync<T>(this IEnumerable<IRule> businessRules, string entityName) where T : ValidationResult
        {
            var rules = await Task.WhenAll(businessRules.Select(rule => rule.ValidateAsync()));

            return rules.Where(rule => !rule.IsValid)
                        .Select(rule =>
                        {
                            return Activator.CreateInstance(typeof(T), rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }) as T;
                        });
        }

        /// <summary>
        /// Invokes supplied rules based on the successful execution of all of these rules
        /// </summary>
        /// <returns>
        /// An array with a single rule
        /// </returns>
        public static IRule IfAllValidThenValidate(this IEnumerable<IRule> r, params IRule[] rules)
        {
            return new ValidRule().IfValidThenValidate(r.ToArray()).IfValidThenValidate(rules);
        }

        /// <summary>
        /// Wraps the rule in an array
        /// </summary>
        /// <returns>
        /// An array with a single rule
        /// </returns>
        public static IEnumerable<IRule> ToArray(this IRule rule)
        {
            return new[] { rule };
        }
    }

    internal class ValidRule: RuleBase
    {
    }
}
