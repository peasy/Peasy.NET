using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface IValidationOperation<T>
    {
        ///
        IEnumerable<ValidationResult> Results { get; }

        ///
        bool CanContinue { get; }

        ///
        Func<Task<T>> CompleteCommandExecutionAsync { get; }
    }

    ///
    public class ValidationOperation<T> : IValidationOperation<T>
    {
        ///
        private Func<Task<T>> _continuationFunction;

        ///
        public ValidationOperation(IEnumerable<ValidationResult> results, Func<Task<T>> completionFunction)
        {
            Results = results;
            CompleteCommandExecutionAsync = completionFunction;
        }

        ///
        public virtual bool CanContinue => !(Results.Any());

        ///
        public virtual IEnumerable<ValidationResult> Results { get; set; }

        ///
        public Func<Task<T>> CompleteCommandExecutionAsync
        {
            get { return CanContinue ? _continuationFunction : null; }
            private set { _continuationFunction = value; }
        }
    }

    /// <summary>
    /// </summary>
    public interface ISupportValidation<T>
    {
        ///
        Task<ValidationOperation<T>> ValidateAsync();
    }

}