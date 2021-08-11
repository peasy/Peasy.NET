using Peasy.Rules;
using System;

namespace Peasy.Extensions
{
    /// <summary>
    /// An extensions class used to perform common tasks against all types.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Returns a new <see cref="ValueRequiredRule"/> based on the type of object being created from.
        /// </summary>
        /// <typeparam name="T">Supports
        /// <see cref="System.String" />,
        /// <see cref="System.UInt64" />,
        /// <see cref="System.UInt32" />,
        /// <see cref="System.Decimal" />,
        /// <see cref="System.Double" />,
        /// <see cref="System.Guid" />
        /// </typeparam>
        /// <param name="value">The value to create the rule from.</param>
        /// <param name="fieldName">The name of the required field.</param>
        /// <returns>A constructed rule of type <see cref="ValueRequiredRule"/></returns>
        public static IRule CreateValueRequiredRule<T>(this T value, string fieldName)
        {
            if (typeof(T) == typeof(String) && value == null)
            {
                return new ValueRequiredRule(string.Empty, fieldName);
            }

            var underlyingType = Nullable.GetUnderlyingType(typeof(T));
            if (underlyingType != null)
            {
                var parsed = value ?? Activator.CreateInstance(underlyingType);
                return ValueRequiredRule(parsed, fieldName);
            }

            return ValueRequiredRule(value, fieldName);
        }

        private static IRule ValueRequiredRule<T>(T value, string fieldName)
        {
            return value switch
            {
                string stringValue => new ValueRequiredRule(stringValue, fieldName),
                long longValue => new ValueRequiredRule(longValue, fieldName),
                decimal decimalValue => new ValueRequiredRule(decimalValue, fieldName),
                double doubleValue => new ValueRequiredRule(doubleValue, fieldName),
                int intValue => new ValueRequiredRule(intValue, fieldName),
                Guid _ => new ValueRequiredRule(new Guid(value.ToString()), fieldName),
                _ => null
            };
        }
    }
}
