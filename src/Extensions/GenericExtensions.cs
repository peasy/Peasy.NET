using Peasy.Rules;
using System;

namespace Peasy.Extensions
{
    public static class GenericExtensions
    {
        public static IRule CreateValueRequiredRule<T>(this T value, string fieldName)
        {
            if (value is string)
                return new ValueRequiredRule(value as string, fieldName);
            else if (value is long)
                return new ValueRequiredRule(Convert.ToInt64(value), fieldName);
            else if (value is decimal)
                return new ValueRequiredRule(Convert.ToDecimal(value), fieldName);
            else if (value is int)
                return new ValueRequiredRule(Convert.ToInt32(value), fieldName);
            else if (value is Guid)
                return new ValueRequiredRule(new Guid(value.ToString()), fieldName);
            return null;
        }
    }
}
