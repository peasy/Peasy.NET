using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface IRulesContainer
    {
        /// <summary>
        /// Asynchronously returns a list of configured rules
        /// </summary>
        Task<IEnumerable<IRule>> GetRulesAsync();

        /// <summary>
        /// Synchronously returns returns a list of configured rules
        /// </summary>
        IEnumerable<IRule> GetRules();
    }
}
