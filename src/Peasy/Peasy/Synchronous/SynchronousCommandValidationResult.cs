using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Synchronous
{
    /// <inheritdoc cref="ICommandValidationResult"/>
    public class SynchronousCommandValidationResult : CommandValidationResultBase<Func<ExecutionResult>>, ISynchronousCommandValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the Peasy.SynchronousCommandValidationResult class with a list of potential errors and a command completion function.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A function that allows the continuation of command execution.</param>
        public SynchronousCommandValidationResult(IEnumerable<ValidationResult> errors, Func<ExecutionResult> continuationFunction) : base(errors, continuationFunction)
        {
            _continuationFunction = continuationFunction;
        }

        /// <inheritdoc cref="ISynchronousCommandValidationResult.CompleteCommandExecution"/>
        public virtual Func<ExecutionResult> CompleteCommandExecution => base.ContinuationFunction;
    }

    /// <inheritdoc cref="ISynchronousCommandValidationResult{T}"/>
    public class SynchronousCommandValidationResult<T> : CommandValidationResultBase<Func<ExecutionResult<T>>>, ISynchronousCommandValidationResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the Peasy.SynchronousCommandValidationResult class with a list of potential errors and a command completion function.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A function that allows the continuation of command execution.</param>
        public SynchronousCommandValidationResult(IEnumerable<ValidationResult> errors, Func<ExecutionResult<T>> continuationFunction) : base(errors, continuationFunction)
        {
            _continuationFunction = continuationFunction;
        }

        /// <inheritdoc cref="ISynchronousCommandValidationResult{T}.CompleteCommandExecution"/>
        public virtual Func<ExecutionResult<T>> CompleteCommandExecution => base.ContinuationFunction;
    }

}