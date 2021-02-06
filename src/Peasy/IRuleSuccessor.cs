using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// Represents a list of rules that have been configured for validation upon successful validation of a parent rule.
    /// </summary>
    public interface IRuleSuccessor<T> : IEnumerable<T>
    {
        /// <summary>
        /// Returns a list of rules that will be validated upon successful validation of the parent rule.
        /// </summary>
        IEnumerable<T> Rules { get; }
    }
}
