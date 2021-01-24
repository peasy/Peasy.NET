using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Exposes validation results based on rule executions.
    /// </summary>
    public interface IValidationErrorsContainer
    {
        /// <summary>
        /// Returns a potential list of <see cref="ValidationResult"/> resulting from rule executions.
        /// </summary>
        /// <returns>
        /// A list of <see cref="ValidationResult"/>.
        /// </returns>
        IEnumerable<ValidationResult> GetErrors();

        /// <summary>
        /// Returns a potential list of <see cref="ValidationResult"/> resulting from rule executions.
        /// </summary>
        /// An awaitable list of <see cref="ValidationResult"/>.
        Task<IEnumerable<ValidationResult>> GetErrorsAsync();
    }

}
