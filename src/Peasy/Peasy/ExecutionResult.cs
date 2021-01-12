using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy
{
    /// <summary>
    /// Defines the result of a command's execution
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// </summary>
        public IEnumerable<ValidationResult> Errors { get; set; }
    }

    /// <summary>
    /// Defines the result of a command's execution and returns a value of T
    /// </summary>
    public class ExecutionResult<T> : ExecutionResult
    {
        /// <summary>
        /// </summary>
        public T Value { get; set; }
    }
}
