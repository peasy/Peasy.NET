using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Extensions
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
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>The name of the property.</returns>
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
            }
          
            //var getMethod = property.GetMethod(true);
            //if (getMethod.IsStatic)
            //{
            //    throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
            //}

            return memberExpression.Member.Name;
        }

        public static string ExtractPropertyName<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
            }
            //var getMethod = property.GetMethod();
            //if (getMethod.IsStatic)
            //{
            //    throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
            //}

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Extracts the value of the property referenced in the provided expression.
        /// </summary>
        /// <typeparam name="T">The type for the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>The value contained in the property.</returns>
        public static T ExtractPropertyValue<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("The expression is not accessing a property.", "propertyExpression");
            }

            return propertyExpression.Compile()();
        }
    }
}
