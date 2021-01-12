using Peasy.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Peasy.Extensions
{
    /// <summary>
    /// An extensions class used to perform common tasks against domain objects.
    /// </summary>
    public static class DomainObjectExtensions
    {
        private static ConcurrentDictionary<Type, PropertyInfo[]> _cachedNonEditableProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();
        private static ConcurrentDictionary<Type, PropertyInfo[]> _cachedForeignKeyProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Performs validation against a supplied object marked with attributes from the <see cref="System.ComponentModel"/> namespace.
        /// </summary>
        /// <typeparam name="T">Any type that inherits <see cref="System.Object"/>.</typeparam>
        /// <param name="domainObject">The object to perform validation against.</param>
        /// <returns>An enumerable source of <see cref="ValidationResult"/>.</returns>
        public static IEnumerable<ValidationResult> GetValidationErrors<T>(this T domainObject) where T : new()
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(domainObject);
            Validator.TryValidateObject(domainObject, context, validationResults, true);
            return validationResults;
        }

        /// <summary>
        /// Returns the name of the supplied type or the value specified by <see cref="Peasy.Attributes.PeasyDisplayNameAttribute"/>if applied to the type.
        /// </summary>
        /// <typeparam name="T">Any type that inherits <see cref="System.Object"/></typeparam>
        /// <param name="domainObject">The object to retrieve the class name from</param>
        /// <returns>A friendly name describing the type</returns>
        public static string ClassName<T>(this T domainObject)
        {
            var type = domainObject.GetType().GetTypeInfo();

            return type.GetCustomAttributes(true)
                .FirstOrDefault(a => a is PeasyDisplayNameAttribute) is PeasyDisplayNameAttribute displayAttribute ? displayAttribute.DisplayName : type.Name;
        }

        /// <summary>
        /// Changes property values from 0 to NULL for any <see cref="System.Nullable"/>(<see cref="System.Int32"/>) property marked with the <see cref="PeasyForeignKeyAttribute"/>
        /// </summary>
        /// <typeparam name="T">Any type that inherits <see cref="System.Object"/></typeparam>
        /// <param name="domainObject">The object to perform the null operations against</param>
        public static void RevertForeignKeysFromZeroToNull<T>(this T domainObject)
        {
            var foreignKeyProperties = domainObject.GetCachedForeignKeyProperties();

            foreach (var property in foreignKeyProperties)
            {
                if (property.GetValue(domainObject) != null)
                {
                    if (int.TryParse(property.GetValue(domainObject).ToString(), out var id))
                    {
                        if (id == 0)
                            property.SetValue(domainObject, null);
                    }
                }
            }
        }

        /// <summary>
        /// Sets any property to the original value when it is marked with the <see cref="EditableAttribute"/> set to false.
        /// </summary>
        /// <typeparam name="T">Any type that inherits <see cref="System.Object"/></typeparam>
        /// <param name="changedObject">The object to perform the revert operations against</param>
        /// <param name="originalObject">The object to perform the revert operations from</param>
        public static void RevertNonEditableValues<T>(this T changedObject, T originalObject)
        {
            var nonEditableProperties = changedObject.GetCachedNonEditableProperties();

            foreach (var property in nonEditableProperties)
            {
#if DEBUG
                var changedValue = property.GetValue(changedObject);
#endif
                var originalValue = property.GetValue(originalObject);
                property.SetValue(changedObject, originalValue);

#if DEBUG
                Debug.WriteLine(
                    $"REVERTED NON-EDITABLE PROPERTY {changedObject.ClassName()}.{property.Name} FROM {changedValue} TO {originalValue}");
#endif
            }
        }

        private static IEnumerable<PropertyInfo> GetCachedForeignKeyProperties<T>(this T domainObject)
        {
            var type = typeof(T);
            if (!_cachedForeignKeyProperties.ContainsKey(type))
            {
                var foreignKeyProps = type.GetRuntimeProperties()
                                          .Where(p => p.GetCustomAttributes(typeof(PeasyForeignKeyAttribute), true).Any())
                                          .Where(p => p.PropertyType == typeof(Nullable<int>));

                _cachedForeignKeyProperties[type] = foreignKeyProps.ToArray();
            }
            return _cachedForeignKeyProperties[type];
        }

        private static IEnumerable<PropertyInfo> GetCachedNonEditableProperties<T>(this T domainObject)
        {
            var type = typeof(T);

            if (!_cachedNonEditableProperties.ContainsKey(type))
            {
                var nonEditableProperties = type.GetRuntimeProperties()
                                                .Where(p => p.GetCustomAttributes(typeof(EditableAttribute), true)
                                                             .Cast<EditableAttribute>()
                                                             .Any(a => a.AllowEdit == false));

                _cachedNonEditableProperties[type] = nonEditableProperties.ToArray();
            }
            return _cachedNonEditableProperties[type];
        }
    }
}
