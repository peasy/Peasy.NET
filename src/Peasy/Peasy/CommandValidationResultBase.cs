using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    ///
    public abstract class CommandValidationResultBase
    {
        ///
        public virtual bool CanContinue => !(Results.Any());

        ///
        public virtual IEnumerable<ValidationResult> Results { get; set; }
    }

    ///
    public class CommandValidationResult<T> : CommandValidationResultBase, ICommandValidationResult<T>
    {
        ///
        private Func<Task<T>> _continuationFunction;

        ///
        public CommandValidationResult(IEnumerable<ValidationResult> results, Func<Task<T>> completionFunction)
        {
            Results = results;
            CompleteCommandExecutionAsync = completionFunction;
        }

        ///
        public Func<Task<T>> CompleteCommandExecutionAsync
        {
            get { return CanContinue ? _continuationFunction : null; }
            private set { _continuationFunction = value; }
        }
    }
}