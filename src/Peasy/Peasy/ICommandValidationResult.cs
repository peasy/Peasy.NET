using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Represents the result of a command validation operation.
    /// </summary>
    public interface ICommandValidationResult<T>
    {
        /// <summary>
        /// Represents a list of errors if any business rules fail validation.
        /// </summary>
        /// <returns>A list of errors if any rules failed validation.</returns>
        IEnumerable<ValidationResult> Errors { get; }

        /// <summary>
        /// Determines whether the validation result will allow continuation of command execution via <see cref="CompleteCommandExecutionAsync"/>.
        /// </summary>
        /// <remarks>Command execution continuance can only happen if the validations produced no errors.</remarks>
        /// <returns><see langword="true"/> if the result can complete command execution, otherwise <see langword="false"/>.</returns>
        bool CanContinue { get; }

        /// <summary>
        /// A function that allows continuation of command execution after successful rule validations are performed.
        /// </summary>
        /// <remarks>Command execution continuance can only happen if the validations produced no errors.</remarks>
        /// <returns>A function that when invoked, completes the command execution pipeline.</returns>
        Func<Task<T>> CompleteCommandExecutionAsync { get; }
    }
}