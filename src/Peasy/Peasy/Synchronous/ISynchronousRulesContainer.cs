using System.Collections.Generic;

namespace Peasy.Synchronous
{
    /// <summary>
    /// Exposes configured validation and business rules.
    /// </summary>
    public interface ISynchronousRulesContainer
    {
        /// <summary>
        /// Composes a list of business and validation rules.
        /// </summary>
        /// <returns>
        /// A list of <see cref="IRule"/>.
        /// </returns>
        IEnumerable<ISynchronousRule> GetRules();
    }
}
