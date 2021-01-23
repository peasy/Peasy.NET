using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Represents a logical unit of work
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Synchronously orchestrates command initialization, rule execution, and command execution.
        /// </summary>
        /// <returns>
        /// An <see cref="ExecutionResult"/>.
        /// </returns>
        ExecutionResult Execute();

        /// <summary>
        /// Asynchronously orchestrates command initialization, rule execution, and command execution.
        /// </summary>
        /// An awaitable <see cref="ExecutionResult"/>.
        Task<ExecutionResult> ExecuteAsync();
    }

    /// <summary>
    /// Represents a logical unit of work
    /// </summary>
    public interface ICommand<T>
    {
        /// <summary>
        /// Synchronously orchestrates command initialization, rule execution, and command execution.
        /// </summary>
        /// An <see cref="ExecutionResult{T}"/>.
        ExecutionResult<T> Execute();

        /// <summary>
        /// Asynchronously orchestrates command initialization, rule execution, and command execution.
        /// </summary>
        /// An awaitable <see cref="ExecutionResult{T}"/>.
        Task<ExecutionResult<T>> ExecuteAsync();
    }
}
