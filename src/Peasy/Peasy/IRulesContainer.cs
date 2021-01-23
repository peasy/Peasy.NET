using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Exposes configured validation and business rules.
    /// </summary>
    public interface IRulesContainer
    {
        /// <summary>
        /// Returns a list of configured rules.
        /// </summary>
        /// <returns>
        /// A list of <see cref="IRule"/>.
        /// </returns>

        IEnumerable<IRule> GetRules();

        /// <summary>
        /// Returns a list of configured rules.
        /// </summary>
        /// An awaitable list of <see cref="IRule"/>.
        Task<IEnumerable<IRule>> GetRulesAsync();
    }
}
