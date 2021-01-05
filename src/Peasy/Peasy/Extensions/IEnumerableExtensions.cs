using System;
using System.Collections.Generic;

namespace Peasy.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> coll, Action<T> func)
        {
            foreach (var v in coll) func?.Invoke(v);
            return coll;
        }
    }
}
