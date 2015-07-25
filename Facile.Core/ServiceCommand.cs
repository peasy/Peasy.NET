using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Core
{
    public class ServiceCommand : Command
    {
        private Action _beforeExecuteMethod;
        private Action _executeMethod;
        private Func<Task> _executeAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getValidationResultsMethod;
        private Func<IEnumerable<ValidationResult>> _getBusinessRulesResultsMethod;

        public ServiceCommand(Action beforeExecuteMethod, Action executeMethod, Func<Task> executeAsyncMethod, Func<IEnumerable<ValidationResult>> getValidationResultsMethod, Func<IEnumerable<ValidationResult>> getBusinessRulesResultsMethod)
        {
            _beforeExecuteMethod = beforeExecuteMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesResultsMethod = getBusinessRulesResultsMethod;
            _getValidationResultsMethod = getValidationResultsMethod;
        }

        protected override void OnInitialization()
        {
            _beforeExecuteMethod();
        }

        protected override void OnExecute()
        {
            _executeMethod();
        }

        protected override async Task OnInitializationAsync()
        {
            _beforeExecuteMethod();
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

    public class ServiceCommand<T> : Command<T>
    {
        private Action _beforeExecuteMethod;
        private Func<T> _executeMethod;
        private Func<Task<T>> _executeAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getValidationResultsMethod;
        private Func<IEnumerable<ValidationResult>> _getBusinessRulesResultsMethod;

        public ServiceCommand(Action beforeExecuteMethod, Func<T> executeMethod, Func<Task<T>> executeAsyncMethod, Func<IEnumerable<ValidationResult>> getValidationResultsMethod, Func<IEnumerable<ValidationResult>> getBusinessRulesResultsMethod)
        {
            _beforeExecuteMethod = beforeExecuteMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesResultsMethod = getBusinessRulesResultsMethod;
            _getValidationResultsMethod = getValidationResultsMethod;
        }

        public ServiceCommand(Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
            : this(() => {}, executeMethod, executeAsyncMethod, () => { return Enumerable.Empty<ValidationResult>(); }, () => { return Enumerable.Empty<ValidationResult>(); })
        {
        }

        public ServiceCommand(Func<T> executeMethod, Func<Task<T>> executeAsyncMethod, Func<IEnumerable<ValidationResult>> getValidationResultsMethod, Func<IEnumerable<ValidationResult>> getBusinessRulesResultsMethod)
            : this(() => {}, executeMethod, executeAsyncMethod, getValidationResultsMethod, getBusinessRulesResultsMethod)
        {
        }

        protected override void OnInitialization()
        {
            _beforeExecuteMethod();
        }

        protected override T OnExecute()
        {
            var result = _executeMethod();
            return result;
        }

        protected override async Task OnInitializationAsync()
        {
            _beforeExecuteMethod();
        }

        protected async override Task<T> OnExecuteAsync()
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
