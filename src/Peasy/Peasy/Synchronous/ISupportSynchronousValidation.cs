using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Synchronous
{
    /// <summary>
    /// </summary>
    public interface ISupportSynchronousValidation
    {
        /// <summary>
        /// Returns a potential list of <see cref="ValidationResult"/> resulting from rule executions.
        /// </summary>
        /// <returns>
        /// A list of <see cref="ValidationResult"/>.
        /// </returns>
        IEnumerable<ValidationResult> Validate();
    }

}