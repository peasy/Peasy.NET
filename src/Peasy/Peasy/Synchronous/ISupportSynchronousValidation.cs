using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Peasy.Synchronous
{
    /// <summary>
    /// </summary>
    public interface ISynchronousValidationOperation<T>
    {
        ///
        IEnumerable<ValidationResult> Results { get; }

        ///
        bool CanContinue { get; }

        ///
        Func<T> CompleteCommandExecution { get; }
    }

    /// <summary>
    /// </summary>
    public interface ISupportSynchronousValidation<T>
    {
        ///
        SynchronousValidationOperation<T> Validate();
    }

    ///
    public class SynchronousValidationOperation<T> : ISynchronousValidationOperation<T>
    {
        ///
        private Func<T> _continuationFunction;

        ///
        public SynchronousValidationOperation(IEnumerable<ValidationResult> results, Func<T> completionFunction)
        {
            Results = results;
            CompleteCommandExecution = completionFunction;
        }

        ///
        public virtual bool CanContinue => !(Results.Any());

        ///
        public virtual IEnumerable<ValidationResult> Results { get; set; }

        ///
        public Func<T> CompleteCommandExecution
        {
            get { return CanContinue ? _continuationFunction : null; }
            private set { _continuationFunction = value; }
        }
    }


}