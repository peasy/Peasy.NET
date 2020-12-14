using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// Represents a rule that can be configured to contain successive rules to be executed after successful execution of a rule
    /// </summary>
    public interface IRuleSuccessorsContainer
    {
        /// <summary>
        /// Returns the rule lists that have been configured to run after successful execution of a rule
        /// </summary>
        IEnumerable<IRuleSuccessor> GetSuccessors();
    }
}
