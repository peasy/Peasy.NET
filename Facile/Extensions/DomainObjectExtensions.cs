using Facile.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Facile.Extensions
{
    public static class DomainObjectExtensions
    {
        private static ConcurrentDictionary<Type, PropertyInfo[]> _cachedNonEditableProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();
        private static ConcurrentDictionary<Type, PropertyInfo[]> _cachedForeignKeyProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public static string ClassName<T>(this T domainObject) 
        {
            var type = domainObject.GetType().GetTypeInfo();
            var displayAttribute = type.GetCustomAttributes(true)
                                       .FirstOrDefault(a => a is FacileDisplayNameAttribute) as FacileDisplayNameAttribute;
            
            if (displayAttribute != null)
                return displayAttribute.DisplayName;

            return type.Name;
        }

        /// <summary>
        /// Changes property values from 0 to NULL for any Nullable<int> property marked with the FacileForeignKeyAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domainObject"></param>
        public static void RevertForeignKeysFromZeroToNull<T>(this T domainObject) 
        {
            var foreignKeyProperties = domainObject.GetCachedForeignKeyProperties();

            foreach (var property in foreignKeyProperties)
            {
                int id = 0;
                if (property.GetValue(domainObject) != null)
                {
                    if (int.TryParse(property.GetValue(domainObject).ToString(), out id))
                    {
                        if (id == 0)
                            property.SetValue(domainObject, null);
                    }
                }
            }
        }

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
                Debug.WriteLine(string.Format("REVERTED NON-EDITABLE PROPERTY {0}.{1} FROM {2} TO {3}", changedObject.ClassName(), property.Name, changedValue, originalValue));
#endif
            }
        }

        public static IEnumerable<PropertyInfo> GetCachedForeignKeyProperties<T>(this T domainObject) 
        {
            var type = typeof(T);
            if (!_cachedForeignKeyProperties.ContainsKey(type))
            {
                var foreignKeyProps = type.GetTypeInfo().DeclaredProperties
                                          .Where(p => p.GetCustomAttributes(typeof(FacileForeignKeyAttribute), true)
                                                       .Any())
                    // Enforce that only properties of type Nullable<int> marked with the FacileForeignKey attribute are selected
                                          //.Where(p => p.PropertyType.gener p.PropertyType.IsGenericType)
                                          .Where(p => p.PropertyType == typeof(Nullable<int>));

                _cachedForeignKeyProperties[type] = foreignKeyProps.ToArray();
            }
            return _cachedForeignKeyProperties[type];
        }

        public static IEnumerable<PropertyInfo> GetCachedNonEditableProperties<T>(this T domainObject) 
        {
            var type = typeof(T); 
            if (!_cachedNonEditableProperties.ContainsKey(type))
            {
                var nonEditableProperties = type.GetTypeInfo().DeclaredProperties
                                                .Where(p => p.GetCustomAttributes(typeof(EditableAttribute), true)
                                                             .Cast<EditableAttribute>()
                                                             .Where(a => a.AllowEdit == false)
                                                             .Any());

                _cachedNonEditableProperties[type] = nonEditableProperties.ToArray();
            }
            return _cachedNonEditableProperties[type];
        }
    }
}
