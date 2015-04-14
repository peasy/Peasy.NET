using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Facile
{
    public class ReturnsIEnumerableServiceCommand<T> : ReturnsIEnumerableCommandBase<T>
    {
        private Func<IEnumerable<T>> _executeMethod;
        private Func<Task<IEnumerable<T>>> _executeAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getValidationResultsMethod;
        private Func<IEnumerable<ValidationResult>> _getBusinessRulesResultsMethod;

        public ReturnsIEnumerableServiceCommand(Func<IEnumerable<T>> executeMethod, Func<Task<IEnumerable<T>>> executeAsyncMethod, Func<IEnumerable<ValidationResult>> getValidationResultsMethod, Func<IEnumerable<ValidationResult>> getBusinessRulesResultsMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesResultsMethod = getBusinessRulesResultsMethod;
            _getValidationResultsMethod = getValidationResultsMethod;
        }

        public ReturnsIEnumerableServiceCommand(Func<IEnumerable<T>> executeMethod, Func<Task<IEnumerable<T>>> executeAsyncMethod)
            : this(executeMethod, executeAsyncMethod, () => { return Enumerable.Empty<ValidationResult>(); }, () => { return Enumerable.Empty<ValidationResult>(); })
        {
        }

        protected override IEnumerable<T> OnExecute()
        {
            var result = _executeMethod();
            return result;
        }

        protected async override Task<IEnumerable<T>> OnExecuteAsync()
        {
            var result = await _executeAsyncMethod();
            return result;
        }

        public override IEnumerable<ValidationResult> GetValidationErrors()
        {
            foreach (var result in _getValidationResultsMethod())
                yield return result;

            foreach (var result in _getBusinessRulesResultsMethod())
                yield return result;
        }
    }
}
