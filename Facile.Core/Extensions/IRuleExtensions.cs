using Facile.Core.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Facile.Core
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

        public static IRule IfAllValidThenValidate(this IEnumerable<IRule> r, params IRule[] rules)
        {
            return new ValidRuleContainer().IfValidThenValidate(r.ToArray()).IfValidThenValidate(rules);
        }
    }

    internal class ValidRuleContainer : RuleBase
    {
        protected override void OnValidate()
        {
        }
    }
}
