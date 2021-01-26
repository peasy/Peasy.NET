using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface ISupportValidation
    {
        /// <summary>
        /// Returns a potential list of <see cref="ValidationResult"/> resulting from rule executions.
        /// </summary>
        /// An awaitable list of <see cref="ValidationResult"/>.
        Task<IEnumerable<ValidationResult>> ValidateAsync();
    }
}