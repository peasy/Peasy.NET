using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Peasy.Synchronous;

namespace Peasy.Extensions
{
    /// <summary>
    /// An extensions class used to perform common tasks against types <see cref="IRule"/> and <see cref="ISynchronousRule"/>.
    /// </summary>
    public static class IRuleExtensions
    {
        /// <summary>
        /// Asynchronously invokes the rules.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> for any failed rules.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="IRule"/> to execute.</param>
        /// <param name="entityName">The name to assign to the <see cref="System.ComponentModel.DataAnnotations.ValidationResult.MemberNames" /> return values.</param>
        public static async Task<IEnumerable<ValidationResult>> ValidateAllAsync(this IEnumerable<IRule> businessRules, string entityName)
        {
            var rules = new List<IRule>();
            // NOTE: Some database providers fail when requesting multiple async db operations.  Favor a foreach loop over Task.WhenAll here.
            foreach (var rule in businessRules) rules.Add(await rule.ExecuteAsync());
            return rules.Where(rule => !rule.IsValid)
                        .Select(rule => new ValidationResult(rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }));
        }

        /// <summary>
        /// Asynchronously invokes the rules.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> for any failed rules.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="IRule"/> to execute.</param>
        public static Task<IEnumerable<ValidationResult>> ValidateAllAsync(this IEnumerable<IRule> businessRules)
        {
            return IRuleExtensions.ValidateAllAsync(businessRules, null);
        }

        /// <summary>
        /// Asynchronously invokes the rule.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> if the rule fails execution.
        /// </returns>
        /// <param name="businessRule">A rule that implements <see cref="IRule"/>.</param>
        public static Task<IEnumerable<ValidationResult>> ValidateAsync(this IRule businessRule)
        {
            return IRuleExtensions.ValidateAllAsync(businessRule.ToArray(), null);
        }

        /// <summary>
        /// Asynchronously invokes the rule.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> if the rule fails execution.
        /// </returns>
        /// <param name="businessRule">A rule that implements <see cref="IRule"/>.</param>
        /// <typeparam name="T">A type that inherits from <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/>.</typeparam>
        public static Task<IEnumerable<T>> ValidateAsync<T>(this IRule businessRule) where T : ValidationResult
        {
            return ValidateAllAsync<T>(businessRule.ToArray(), null);
        }

        /// <summary>
        /// Asynchronously invokes the rules.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> for any failed rules.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="IRule"/> to execute.</param>
        /// <typeparam name="T">A type that inherits from <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/>.</typeparam>
        public static Task<IEnumerable<T>> ValidateAllAsync<T>(this IEnumerable<IRule> businessRules) where T : ValidationResult
        {
            return ValidateAllAsync<T>(businessRules, null);
        }

        /// <summary>
        /// Asynchronously invokes the rules.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> for any failed rules.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="IRule"/> to execute.</param>
        /// <typeparam name="T">A type that inherits from <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/>.</typeparam>
        /// <param name="entityName">The name to assign to the <see cref="System.ComponentModel.DataAnnotations.ValidationResult.MemberNames" /> return values.</param>
        public static async Task<IEnumerable<T>> ValidateAllAsync<T>(this IEnumerable<IRule> businessRules, string entityName) where T : ValidationResult
        {
            var rules = new List<IRule>();
            // NOTE: Some database providers fail when requesting multiple async db operations.  Favor a foreach loop over Task.WhenAll here.
            foreach (var rule in businessRules) rules.Add(await rule.ExecuteAsync());
            return rules.Where(rule => !rule.IsValid)
                        .Select(rule =>
                        {
                            return Activator.CreateInstance(typeof(T), rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }) as T;
                        });
        }

        /// <summary>
        /// Invokes supplied rules based on the successful execution of all of these rules.
        /// </summary>
        /// <returns>
        /// An single rule of type <see cref="IRule"/>.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="IRule"/> to execute.</param>
        /// <param name="rules">An enumerable list of <see cref="IRule"/> to execute if all of these rules have successful execution.</param>
        public static IRule IfAllValidThenValidate(this IEnumerable<IRule> businessRules, params IRule[] rules)
        {
            return new ValidRule().IfValidThenValidate(businessRules.ToArray()).IfValidThenValidate(rules);
        }

        /// <summary>
        /// Invokes supplied rules based on the successful execution of all of these rules.
        /// </summary>
        /// <returns>
        /// An single rule of type <see cref="ISynchronousRule"/>.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="ISynchronousRule"/> to execute.</param>
        /// <param name="rules">An enumerable list of <see cref="ISynchronousRule"/> to execute if all of these rules have successful execution.</param>
        public static ISynchronousRule IfAllValidThenValidate(this IEnumerable<ISynchronousRule> businessRules, params ISynchronousRule[] rules)
        {
            return new SynchronousValidRule().IfValidThenValidate(businessRules.ToArray()).IfValidThenValidate(rules);
        }

        /// <summary>
        /// Wraps the rule in an array
        /// </summary>
        /// <returns>
        /// An array with a single rule of type <see cref="IRule"/>
        /// </returns>
        /// <param name="rule">The rule of type <see cref="IRule"/> to wrap in array.</param>
        public static IEnumerable<IRule> ToArray(this IRule rule)
        {
            return new[] { rule };
        }

        /// <summary>
        /// Synchronously invokes the rules.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> for any failed rules
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="ISynchronousRule"/> to execute</param>
        /// <param name="entityName">The name to assign to the <see cref="System.ComponentModel.DataAnnotations.ValidationResult.MemberNames"/> return value.</param>
        public static IEnumerable<ValidationResult> ValidateAll(this IEnumerable<ISynchronousRule> businessRules, string entityName)
        {
            var rules = businessRules.Select(rule => rule.Execute())
                                     .Where(rule => !rule.IsValid)
                                     .Select(rule => new ValidationResult(rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }));
            return rules;
        }

        /// <summary>
        /// Synchronously invokes the rules.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> for any failed rules.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="ISynchronousRule"/> to execute.</param>
        public static IEnumerable<ValidationResult> ValidateAll(this IEnumerable<ISynchronousRule> businessRules)
        {
            return IRuleExtensions.ValidateAll(businessRules, null);
        }

        /// <summary>
        /// Synchronously invokes the rule.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> if the rule fails execution.
        /// </returns>
        /// <param name="businessRule">A rule that implements <see cref="ISynchronousRule"/>.</param>
        public static IEnumerable<ValidationResult> Validate(this ISynchronousRule businessRule)
        {
            return IRuleExtensions.ValidateAll(new [] { businessRule }, null);
        }

        /// <summary>
        /// Synchronously invokes the rule.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> if the rule fails execution.
        /// </returns>
        /// <param name="businessRule">A rule that implements <see cref="ISynchronousRule"/>.</param>
        /// <typeparam name="T">A type that inherits from <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/>.</typeparam>
        public static IEnumerable<T> Validate<T>(this ISynchronousRule businessRule) where T : ValidationResult
        {
            return ValidateAll<T>(businessRule.ToArray(), null);
        }

        /// <summary>
        /// Synchronously invokes the rules.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> for any failed rules.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="ISynchronousRule"/> to execute.</param>
        /// <typeparam name="T">A type that inherits from <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/>.</typeparam>
        public static IEnumerable<T> ValidateAll<T>(this IEnumerable<ISynchronousRule> businessRules) where T : ValidationResult
        {
            return ValidateAll<T>(businessRules, null);
        }

        /// <summary>
        /// Synchronously invokes the rules.
        /// </summary>
        /// <returns>
        /// List of <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/> for any failed rules.
        /// </returns>
        /// <param name="businessRules">An enumerable list of <see cref="ISynchronousRule"/> to execute.</param>
        /// <typeparam name="T">A type that inherits from <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/>.</typeparam>
        /// <param name="entityName">The name to assign to the <see cref="System.ComponentModel.DataAnnotations.ValidationResult.MemberNames" /> return values.</param>
        public static IEnumerable<T> ValidateAll<T>(this IEnumerable<ISynchronousRule> businessRules, string entityName) where T : ValidationResult
        {
            var rules = businessRules.Select(rule => rule.Execute())
                                     .Where(rule => !rule.IsValid)
                                     .Select(rule =>
                                     {
                                         return Activator.CreateInstance(typeof(T), rule.ErrorMessage, new string[] { entityName ?? rule.Association ?? string.Empty }) as T;
                                     });
            return rules;
        }


        /// <summary>
        /// Wraps the rule in an array
        /// </summary>
        /// <returns>
        /// An array with a single rule of type <see cref="ISynchronousRule"/>
        /// </returns>
        /// <param name="rule">The rule of type <see cref="ISynchronousRule"/> to wrap in array.</param>
        public static IEnumerable<ISynchronousRule> ToArray(this ISynchronousRule rule)
        {
            return new[] { rule };
        }
    }

    internal class ValidRule: RuleBase { }
    internal class SynchronousValidRule: SynchronousRuleBase { }
}
