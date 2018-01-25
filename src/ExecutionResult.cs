using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy
{
    /// <summary>
    /// Defines the result of a command's execution
    /// </summary>
    public class ExecutionResult
    {
        public bool Success { get; set; }
        public IEnumerable<ValidationResult> Errors { get; set; }
    }

    /// <summary>
    /// Defines the result of a command's execution and returns a value of T
    /// </summary>
    public class ExecutionResult<T> : ExecutionResult
    {
        public T Value { get; set; }
    }
}
