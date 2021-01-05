using Peasy.Rules;
using System;

namespace Peasy.Extensions
{
    public static class GenericExtensions
    {
        public static IRule CreateValueRequiredRule<T>(this T value, string fieldName)
        {
            return value switch
            {
                string stringValue => new ValueRequiredRule(stringValue, fieldName),
                long longValue => new ValueRequiredRule(longValue, fieldName),
                decimal decimalValue => new ValueRequiredRule(decimalValue, fieldName),
                int intValue => new ValueRequiredRule(intValue, fieldName),
                Guid _ => new ValueRequiredRule(new Guid(value.ToString()), fieldName),
                _ => null
            };
        }
    }
}
