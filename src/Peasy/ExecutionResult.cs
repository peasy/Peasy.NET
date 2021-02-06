using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy
{
    /// <summary>
    /// Represents the result of a command execution.
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// Indicates whether the command execution was successful.
        /// </summary>
        /// <remarks>
        /// Command execution is successful if all configured business and validation rules pass validation.
        /// </remarks>
        /// <returns><see langword="true"/> if command execution was successful, otherwise <see langword="false"/>.</returns>
        public virtual bool Success { get; set; }

        /// <summary>
        /// Represents a list of errors if any business rules fail validation.
        /// </summary>
        /// <returns>A list of errors if <see cref="Success"/> is <see langword="false"/>, otherwise <see langword="null"/>.</returns>
        public virtual IEnumerable<ValidationResult> Errors { get; set; }
    }

    /// <summary>
    /// Represents the result of a command execution.
    /// </summary>
    public class ExecutionResult<T> : ExecutionResult
    {
        /// <summary>
        /// Represents a domain object or resource as the result of successful command execution.
        /// </summary>
        /// <returns>A resource of type <typeparamref name="T"/>.</returns>
        public virtual T Value { get; set; }
    }
}
