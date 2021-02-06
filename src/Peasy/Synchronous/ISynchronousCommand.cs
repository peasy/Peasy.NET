namespace Peasy.Synchronous
{
    /// <summary>
    /// Represents a logical unit of work.
    /// </summary>
    public interface ISynchronousCommand
    {
        /// <summary>
        /// Orchestrates command initialization, rule execution, and application logic.
        /// </summary>
        /// <returns>
        /// An execution result.
        /// </returns>
        ExecutionResult Execute();
    }

    /// <summary>
    /// Represents a logical unit of work.
    /// </summary>
    public interface ISynchronousCommand<T>
    {
        /// <summary>
        /// Orchestrates command initialization, rule execution, and application logic.
        /// </summary>
        /// <returns>
        /// An execution result.
        /// </returns>
        ExecutionResult<T> Execute();
    }
}
