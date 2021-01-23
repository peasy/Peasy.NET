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
        /// Returns a list of validation results as the result of rule execution(s).
        /// </summary>
        /// <returns>
        /// A list of <see cref="ValidationResult"/>.
        /// </returns>
        IEnumerable<ValidationResult> GetErrors();

        /// <summary>
        /// Returns a list of validation results as the result of rule execution(s).
        /// </summary>
        /// An awaitable list of <see cref="ValidationResult"/>.
        Task<IEnumerable<ValidationResult>> GetErrorsAsync();
    }

}
