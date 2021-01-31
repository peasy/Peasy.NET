using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Represents the base class for CommandValidationResult.
    /// </summary>
    public abstract class CommandValidationResultBase
    {
        /// <inheritdoc cref="ICommandValidationResult.CanContinue"/>
        public virtual bool CanContinue => !(Errors.Any());

        /// <inheritdoc cref="ICommandValidationResult.Errors"/>
        public virtual IEnumerable<ValidationResult> Errors { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the Peasy.CommandValidationResultBase.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        public CommandValidationResultBase(IEnumerable<ValidationResult> errors)
        {
            Errors = errors ?? Enumerable.Empty<ValidationResult>();
        }
    }

    /// <inheritdoc cref="ICommandValidationResult"/>
    public class CommandValidationResult : CommandValidationResultBase, ICommandValidationResult
    {
        /// <summary>The command completion function.</summary>
        protected Func<Task<ExecutionResult>> _continuationFunction;

        /// <summary>
        /// Initializes a new instance of the Peasy.CommandValidationResult class with a list of potential errors and a command completion function.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A function that allows the continuation of command execution.</param>
        public CommandValidationResult(IEnumerable<ValidationResult> errors, Func<Task<ExecutionResult>> continuationFunction) : base(errors)
        {
            _continuationFunction = continuationFunction;
        }

        /// <inheritdoc cref="ICommandValidationResult.CompleteCommandExecutionAsync"/>
        public virtual Func<Task<ExecutionResult>> CompleteCommandExecutionAsync
        {
            get
            {
                if (CanContinue) return _continuationFunction;
                throw new InvalidOperationException("Cannot complete command execution because validation was not successful.");
            }
        }
    }

    /// <inheritdoc cref="ICommandValidationResult{T}"/>
    public class CommandValidationResult<T> : CommandValidationResultBase, ICommandValidationResult<T>
    {
        /// <summary>The command completion function.</summary>
        protected Func<Task<ExecutionResult<T>>> _continuationFunction;

        /// <summary>
        /// Initializes a new instance of the Peasy.CommandValidationResult class with a list of potential errors and a command completion function.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A function that allows the continuation of command execution.</param>
        public CommandValidationResult(IEnumerable<ValidationResult> errors, Func<Task<ExecutionResult<T>>> continuationFunction) : base(errors)
        {
            _continuationFunction = continuationFunction;
        }

        /// <inheritdoc cref="ICommandValidationResult{T}.CompleteCommandExecutionAsync"/>
        public virtual Func<Task<ExecutionResult<T>>> CompleteCommandExecutionAsync
        {
            get
            {
                if (CanContinue) return _continuationFunction;
                throw new InvalidOperationException("Cannot complete command execution because validation was not successful.");
            }
        }
    }
}