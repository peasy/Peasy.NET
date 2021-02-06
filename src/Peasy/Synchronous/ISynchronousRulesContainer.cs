using System.Collections.Generic;

namespace Peasy.Synchronous
{
    /// <summary>
    /// Provides access to configured business and validation rules.
    /// </summary>
    public interface ISynchronousRulesContainer
    {
        /// <summary>
        /// Composes a list of business and validation rules.
        /// </summary>
        /// <returns>A list of rules.</returns>
        IEnumerable<ISynchronousRule> GetRules();
    }
}
