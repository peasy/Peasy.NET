﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Peasy
{
    /// <summary>
    /// Represents the base class for CommandValidationResult.
    /// </summary>
    /// <typeparam name="TFunc">Represents a function that can be invoked to complete command execution based on the status of <see cref="CanContinue"/>.</typeparam>
    public abstract class CommandValidationResultBase<TFunc>
    {
        /// <summary>The command completion function.</summary>
        protected TFunc _continuationFunction;

        /// <inheritdoc cref="ICommandValidationResult.CanContinue"/>
        public virtual bool CanContinue => !(Errors.Any());

        /// <inheritdoc cref="ICommandValidationResult.Errors"/>
        public virtual IEnumerable<ValidationResult> Errors { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the Peasy.CommandValidationResultBase.
        /// </summary>
        /// <param name="errors">A potential list of errors resulting from rule executions.</param>
        /// <param name="continuationFunction">A potential list of errors resulting from rule executions.</param>
        public CommandValidationResultBase(IEnumerable<ValidationResult> errors, TFunc continuationFunction)
        {
            Errors = errors ?? Enumerable.Empty<ValidationResult>();
            _continuationFunction = continuationFunction;
        }

        /// <summary>
        /// A function that allows continuation of command execution after successful rule validations are performed.
        /// </summary>
        /// <remarks>Can only be invoked if <see cref="CanContinue"/> returns <see langword="true"/>.</remarks>
        /// <returns>A function that when invoked, continues the command execution pipeline.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when accessed if failed validations exist.</exception>
        public virtual TFunc ContinuationFunction
        {
            get
            {
                if (CanContinue) return _continuationFunction;
                throw new InvalidOperationException("Cannot complete command execution because validation was not successful.");
            }
        }
    }
}