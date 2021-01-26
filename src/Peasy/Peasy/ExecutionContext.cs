using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// Serves as shared state for all pipeline method invocations of a <see cref="Command"/>.
    /// </summary>
    /// <typeparam name="T">Serves to represent a domain object or resource.</typeparam>
    public class ExecutionContext<T>
    {
        /// <summary>
        /// Represents a resource that the current <see cref="Command"/> pipeline is working with.
        /// </summary>
        public T CurrentEntity { get; set; }

        /// <summary>
        /// Represents a state bag that the current <see cref="Command"/> pipeline can access.
        /// </summary>
        /// <remarks>
        /// Use this dictionary to share state between command pipeline method invocations.
        /// </remarks>
        public Dictionary<string, object> Data { get; set; }
    }
}
