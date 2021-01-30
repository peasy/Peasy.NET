namespace Peasy.Synchronous
{
    /// <summary>
    /// Supports the execution of configured rules within a command and the ability to continue command execution based on successful validation results.
    /// </summary>
    public interface ISupportSynchronousCommandValidation
    {
        /// <summary>
        /// Performs validation by invoking configured rules.
        /// </summary>
        /// <returns>A command validation result.</returns>
        ISynchronousCommandValidationResult Validate();
    }

    /// <summary>
    /// Supports the execution of configured rules within a command and the ability to continue command execution based on successful validation results.
    /// </summary>
    public interface ISupportSynchronousCommandValidation<T>
    {
        /// <summary>
        /// Performs validation by invoking configured rules.
        /// </summary>
        /// <returns>A command validation result.</returns>
        ISynchronousCommandValidationResult<T> Validate();
    }
}