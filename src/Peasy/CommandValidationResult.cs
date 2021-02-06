using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Peasy
{
    /// <inheritdoc cref="ICommandValidationResult"/>
    public class CommandValidationResult : CommandValidationResultBase<Func<Task<ExecutionResult>>>, ICommandValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the Peasy.CommandValidationResult class with a list of potential errors and a command completion function.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A function that allows the continuation of command execution.</param>
        public CommandValidationResult(IEnumerable<ValidationResult> errors, Func<Task<ExecutionResult>> continuationFunction) : base(errors, continuationFunction)
        {
            _continuationFunction = continuationFunction;
        }

        /// <inheritdoc cref="ICommandValidationResult.CompleteCommandExecutionAsync"/>
        public Func<Task<ExecutionResult>> CompleteCommandExecutionAsync => base.ContinuationFunction;
    }

    /// <inheritdoc cref="ICommandValidationResult{T}"/>
    public class CommandValidationResult<T> : CommandValidationResultBase<Func<Task<ExecutionResult<T>>>>, ICommandValidationResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the Peasy.CommandValidationResult class with a list of potential errors and a command completion function.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A function that allows the continuation of command execution.</param>
        public CommandValidationResult(IEnumerable<ValidationResult> errors, Func<Task<ExecutionResult<T>>> continuationFunction) : base(errors, continuationFunction)
        {
            _continuationFunction = continuationFunction;
        }

        /// <inheritdoc cref="ICommandValidationResult{T}.CompleteCommandExecutionAsync"/>
        public Func<Task<ExecutionResult<T>>> CompleteCommandExecutionAsync => base.ContinuationFunction;
    }
}