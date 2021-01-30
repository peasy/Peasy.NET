using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// Serves as shared state for all pipeline method invocations of a command within an instance of <see cref="ServiceBase{T, TKey}"/>.
    /// </summary>
    /// <typeparam name="T">Serves to represent a domain object or resource.</typeparam>
    public class ExecutionContext<T>
    {
        /// <summary>
        /// Represents a resource that the current command pipeline is working with.
        /// </summary>
        public T CurrentEntity { get; set; }

        /// <summary>
        /// Represents a state bag that the current command pipeline can access.
        /// </summary>
        /// <remarks>
        /// Use this dictionary to share state between command pipeline method invocations.
        /// </remarks>
        public Dictionary<string, object> Data { get; set; }
    }
}
