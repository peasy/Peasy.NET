using Peasy;
using System;

namespace Orders.com.Core.Extensions
{
    public static class IVersionContainerExtensions
    {
        public static IVersionContainer IncrementVersionByOne(this IVersionContainer container)
        {
            container.Version = (Convert.ToInt64(container.Version) + 1).ToString();
            return container;
        }
    }
}
