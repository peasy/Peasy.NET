using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Peasy
{
    public interface IValidationErrorsContainer
    {
        /// <summary>
        /// Synchronously returns a list of validation results as a result of rule execution(s)
        /// </summary>
        IEnumerable<ValidationResult> GetErrors();

        /// <summary>
        /// Asynchronously returns a list of validation results as a result of rule execution(s)
        /// </summary>
        Task<IEnumerable<ValidationResult>> GetErrorsAsync();
    }

    public interface ICommand : IValidationErrorsContainer
    {
        /// <summary>
        /// Synchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        ExecutionResult Execute();

        /// <summary>
        /// Asynchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        Task<ExecutionResult> ExecuteAsync();
    }

    public interface ICommand<T> : IValidationErrorsContainer
    {
        /// <summary>
        /// Synchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        ExecutionResult<T> Execute();

        /// <summary>
        /// Asynchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        Task<ExecutionResult<T>> ExecuteAsync();
    }
}
