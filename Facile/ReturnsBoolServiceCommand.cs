using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile
{
    public class ReturnsBoolServiceCommand : ReturnsBoolCommandBase
    {
        private Func<bool> _executeMethod;
        private Func<Task<bool>> _executeAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getValidationResultsMethod;
        private Func<IEnumerable<ValidationResult>> _getBusinessRulesResultsMethod;

        public ReturnsBoolServiceCommand(Func<bool> executeMethod, Func<Task<bool>> executeAsyncMethod, Func<IEnumerable<ValidationResult>> getValidationResultsMethod, Func<IEnumerable<ValidationResult>> getBusinessRulesResultsMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesResultsMethod = getBusinessRulesResultsMethod;
            _getValidationResultsMethod = getValidationResultsMethod;
        }

        protected override bool OnExecute()
        {
            var result = _executeMethod();
            return result;
        }

        protected async override Task<bool> OnExecuteAsync()
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
