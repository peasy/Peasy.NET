using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Peasy.Extensions
{
    /// <summary>
    /// A utility class for common tasks to perform against Expressions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Extracts the name of the property referenced in the provided expression.
        /// </summary>
        /// <typeparam name="T">The type for the property.</typeparam>
        /// <param name=nameof(propertyExpression)>The property expression.</param>
        /// <returns>The name of the property.</returns>
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (!(propertyExpression.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("The expression is not a member access expression.", nameof(propertyExpression));
            }

            if (!(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException("The member access expression does not access a property.", nameof(propertyExpression));
            }

            //var getMethod = property.GetMethod(true);
            //if (getMethod.IsStatic)
            //{
            //    throw new ArgumentException("The referenced property is a static property.", nameof(propertyExpression));
            //}

            return memberExpression.Member.Name;
        }

        public static string ExtractPropertyName<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (!(propertyExpression.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("The expression is not a member access expression.", nameof(propertyExpression));
            }

            if (!(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException("The member access expression does not access a property.", nameof(propertyExpression));
            }
            //var getMethod = property.GetMethod();
            //if (getMethod.IsStatic)
            //{
            //    throw new ArgumentException("The referenced property is a static property.", nameof(propertyExpression));
            //}

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Extracts the value of the property referenced in the provided expression.
        /// </summary>
        /// <typeparam name="T">The type for the property.</typeparam>
        /// <param name=nameof(propertyExpression)>The property expression.</param>
        /// <returns>The value contained in the property.</returns>
        public static T ExtractPropertyValue<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (!(propertyExpression.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("The expression is not a member access expression.", nameof(propertyExpression));
            }

            if (!(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException("The expression is not accessing a property.", nameof(propertyExpression));
            }

            return propertyExpression.Compile()();
        }
    }
}
