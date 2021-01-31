using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Peasy.Synchronous
{
    /// <inheritdoc cref="ICommandValidationResult"/>
    public class SynchronousCommandValidationResult : CommandValidationResultBase, ISynchronousCommandValidationResult
    {
        /// <summary>The command completion function.</summary>
        protected Func<ExecutionResult> _continuationFunction;

        /// <summary>
        /// Initializes a new instance of the Peasy.SynchronousCommandValidationResult class with a list of potential errors and a command completion function.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A function that allows the continuation of command execution.</param>
        public SynchronousCommandValidationResult(IEnumerable<ValidationResult> errors, Func<ExecutionResult> continuationFunction) : base(errors)
        {
            _continuationFunction = continuationFunction;
        }

        /// <inheritdoc cref="ISynchronousCommandValidationResult.CompleteCommandExecution"/>
        public virtual Func<ExecutionResult> CompleteCommandExecution
        {
            get
            {
                if (CanContinue) return _continuationFunction;
                throw new InvalidOperationException("Cannot complete command execution because validation was not successful.");
            }
        }
    }

    /// <inheritdoc cref="ISynchronousCommandValidationResult{T}"/>
    public class SynchronousCommandValidationResult<T> : CommandValidationResultBase, ISynchronousCommandValidationResult<T>
    {
        /// <summary>The command completion function.</summary>
        protected Func<ExecutionResult<T>> _continuationFunction;

        /// <summary>
        /// Initializes a new instance of the Peasy.SynchronousCommandValidationResult class with a list of potential errors and a command completion function.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A function that allows the continuation of command execution.</param>
        public SynchronousCommandValidationResult(IEnumerable<ValidationResult> errors, Func<ExecutionResult<T>> continuationFunction) : base(errors)
        {
            _continuationFunction = continuationFunction;
        }

        /// <inheritdoc cref="ISynchronousCommandValidationResult{T}.CompleteCommandExecution"/>
        public virtual Func<ExecutionResult<T>> CompleteCommandExecution
        {
            get
            {
                if (CanContinue) return _continuationFunction;
                throw new InvalidOperationException("Cannot complete command execution because validation was not successful.");
            }
        }
    }

}