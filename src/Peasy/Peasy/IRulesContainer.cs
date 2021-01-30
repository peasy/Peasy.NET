using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Provides access to configured business and validation rules.
    /// </summary>
    public interface IRulesContainer
    {
        /// <summary>
        /// Composes a list of business and validation rules.
        /// </summary>
        /// <returns>An awaitable list of rules.</returns>
        Task<IEnumerable<IRule>> GetRulesAsync();
    }
}
