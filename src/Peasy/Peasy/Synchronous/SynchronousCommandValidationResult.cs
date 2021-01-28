using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Peasy.Synchronous
{
    ///
    public class SynchronousCommandValidationResult<T> : CommandValidationResultBase, ISynchronousCommandValidationResult<T>
    {
        ///
        private Func<T> _continuationFunction;

        ///
        public SynchronousCommandValidationResult(IEnumerable<ValidationResult> results, Func<T> completionFunction)
        {
            Results = results;
            CompleteCommandExecution = completionFunction;
        }

        ///
        public Func<T> CompleteCommandExecution
        {
            get { return CanContinue ? _continuationFunction : null; }
            private set { _continuationFunction = value; }
        }
    }

}