using Facile.Core.Extensions;
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
        private Func<IEnumerable<ValidationResult>> _getErrorsMethod;
        private Func<Task<IEnumerable<ValidationResult>>> _getErrorsAsyncMethod;

        public ServiceCommand(Action beforeExecuteMethod, 
                              Action executeMethod, 
                              Func<Task> executeAsyncMethod, 
                              Func<IEnumerable<ValidationResult>> getErrorsMethod, 
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _beforeExecuteMethod = beforeExecuteMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        public ServiceCommand(Action executeMethod, Func<Task> executeAsyncMethod)
            : this(() => {}, 
                  executeMethod, 
                  executeAsyncMethod, 
                  () => Enumerable.Empty<ValidationResult>(), 
                  () => Task.Run(() => Enumerable.Empty<ValidationResult>()))
        {
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

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return _getErrorsMethod();
        }

        public override async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            var validationResults = await _getErrorsAsyncMethod();
            return validationResults;
        }
    }

    public class ServiceCommand<T> : Command<T>
    {
        private Action _beforeExecuteMethod;
        private Func<T> _executeMethod;
        private Func<Task<T>> _executeAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getValidationRulesMethod;
        private Func<IEnumerable<IRule>> _getBusinessRulesMethod;
        private Func<IEnumerable<ValidationResult>> _getErrorsMethod;
        private Func<Task<IEnumerable<ValidationResult>>> _getErrorsAsyncMethod;

        public ServiceCommand(Action beforeExecuteMethod, 
                              Func<T> executeMethod, 
                              Func<Task<T>> executeAsyncMethod, 
                              Func<IEnumerable<ValidationResult>> getErrorsMethod, 
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _beforeExecuteMethod = beforeExecuteMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        public ServiceCommand(Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
            : this(() => {}, 
                  executeMethod, 
                  executeAsyncMethod, 
                  () => Enumerable.Empty<ValidationResult>(), 
                  () => Task.Run(() => Enumerable.Empty<ValidationResult>()))
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
            return await _executeAsyncMethod();
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return _getErrorsMethod();
        }

        public override async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return await _getErrorsAsyncMethod();
        }
    }
}
