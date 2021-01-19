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
        public virtual bool Success { get; set; }

        /// <summary>
        /// </summary>
        public virtual IEnumerable<ValidationResult> Errors { get; set; }
    }

    /// <summary>
    /// Defines the result of a command's execution and returns a value of T
    /// </summary>
    public class ExecutionResult<T> : ExecutionResult
    {
        /// <summary>
        /// </summary>
        public virtual T Value { get; set; }
    }
}
