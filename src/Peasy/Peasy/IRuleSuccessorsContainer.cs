using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// Represents a rule that can be configured to contain successive rules to be validated after successful validation of this rule.
    /// </summary>
    public interface IRuleSuccessorsContainer<T>
    {
        /// <summary>
        /// Returns the rule successors that have been configured to be validated after successful validation of this rule.
        /// </summary>
        IEnumerable<IRuleSuccessor<T>> GetSuccessors();
    }
}
