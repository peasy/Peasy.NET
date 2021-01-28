using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Supports the execution of configured rules and the ability to continue command execution based on successful results.
    /// </summary>
    public interface ISupportCommandValidation
    {
        /// <summary>
        /// Performs validation by invoking configured rules.
        /// </summary>
        /// <returns>An awaitable command validation result.</returns>
        Task<ICommandValidationResult> ValidateAsync();
    }

    /// <summary>
    /// Supports the execution of configured rules and the ability to continue command execution based on successful results.
    /// </summary>
    public interface ISupportCommandValidation<T>
    {
        /// <summary>
        /// Performs validation by invoking configured rules.
        /// </summary>
        /// <returns>An awaitable command validation result.</returns>
        Task<ICommandValidationResult<T>> ValidateAsync();
    }
}