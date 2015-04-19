using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facile.Core
{
    public abstract class CommandBase : ICommandBase
    {
        public virtual bool CanExecute
        {
            get { return !GetValidationErrors().Any(); }
        }

        public virtual IEnumerable<ValidationResult> GetValidationErrors()
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }

    public interface IAsyncCommand
    {
        Task<bool> CanExecuteAsync();
        Task<IEnumerable<ValidationResult>> GetValidationErrorsAsync();
    }

    public abstract class AsyncCommandBase<T> : IAsyncCommand where T : DomainBase
    {
        private Func<Task<IEnumerable<ValidationResult>>> _getValidationResultsAsyncMethod;
        private Func<Task<IEnumerable<ValidationResult>>> _getBusinessRulesResultsAsyncMethod;

        public AsyncCommandBase(Func<Task<IEnumerable<ValidationResult>>> getValidationResultsAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getBusinessRulesResultsAsyncMethod)
        {
            _getValidationResultsAsyncMethod = getValidationResultsAsyncMethod;
            _getBusinessRulesResultsAsyncMethod = getBusinessRulesResultsAsyncMethod;
        }

        public virtual async Task<bool> CanExecuteAsync()
        {
            var errors = await GetValidationErrorsAsync();
            return !errors.Any();
        }

        public virtual async Task<IEnumerable<ValidationResult>> GetValidationErrorsAsync()
        {
            var validationResults = _getValidationResultsAsyncMethod();
            var businessRulesResults = _getBusinessRulesResultsAsyncMethod();

            var x = await Task.WhenAll(validationResults, businessRulesResults);
            return x.SelectMany(a => a);

            //return validationResults.Concat(businessRulesResults);
        }
    }
}
