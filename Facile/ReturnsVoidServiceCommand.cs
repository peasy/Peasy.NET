using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facile
{
    public class ReturnsVoidServiceCommand : ReturnsVoidCommandBase
    {
        private Action _executeMethod;
        private Func<Task> _executeAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getValidationResultsMethod;
        private Func<IEnumerable<ValidationResult>> _getBusinessRulesResultsMethod;

        public ReturnsVoidServiceCommand(Action executeMethod, Func<Task> executeAsyncMethod, Func<IEnumerable<ValidationResult>> getValidationResultsMethod, Func<IEnumerable<ValidationResult>> getBusinessRulesResultsMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesResultsMethod = getBusinessRulesResultsMethod;
            _getValidationResultsMethod = getValidationResultsMethod;
        }

        public ReturnsVoidServiceCommand(Action executeMethod, Func<Task> executeAsyncMethod)
            : this(executeMethod, executeAsyncMethod, () => { return Enumerable.Empty<ValidationResult>(); }, () => { return Enumerable.Empty<ValidationResult>(); })
        {
        }

        protected override void OnExecute()
        {
            _executeMethod();
        }

        protected async override Task OnExecuteAsync()
        {
            await _executeAsyncMethod();
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
