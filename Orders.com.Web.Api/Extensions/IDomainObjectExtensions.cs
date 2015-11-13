using Peasy.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Orders.com.Web.Api
{
    public static class IDomainObjectExtensions
    {
        public static string ClassName<T>(this T domainObject) where T : IDomainObject
        {
            var type = domainObject.GetType();
            var displayAttribute = type.GetCustomAttributes(true)
                                       .FirstOrDefault(a => a is DisplayNameAttribute) as DisplayNameAttribute;

            if (displayAttribute != null)
                return displayAttribute.DisplayName;

            return type.Name;
        }
    }
}