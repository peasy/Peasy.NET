namespace Peasy.Synchronous
{
    /// <summary>
    /// Represents a business or validation rule whose executed outcome must be valid in order to complete command pipeline execution.
    /// </summary>
    public interface ISynchronousRule
    {
        /// <summary>
        /// Represents an association between this rule and a field or property, which can be especially helpful for displaying validation errors.
        /// </summary>
        string Association { get; }

        /// <summary>Represents the error message resulting from a failed rule execution.</summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Gets a value indicating whether this rule is valid after execution.
        /// </summary>
        /// <returns><see langword="true"/> if this instance is valid, otherwise <see langword="false"/>.</returns>
        bool IsValid { get; }

        /// <summary>
        /// Asynchronously executes this rule.
        /// </summary>
        /// <returns>An awaitable reference to this rule.</returns>
        ISynchronousRule Execute();
    }
}
