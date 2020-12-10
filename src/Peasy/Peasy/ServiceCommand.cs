using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    public class ServiceCommand : Command
    {
        protected Action _initializationMethod;
        protected Func<Task> _initializationAsyncMethod;
        protected Func<IEnumerable<ValidationResult>> _getErrorsMethod;
        protected Func<Task<IEnumerable<ValidationResult>>> _getErrorsAsyncMethod;
        protected Action _executeMethod;
        protected Func<Task> _executeAsyncMethod;
        protected Func<IEnumerable<IRule>> _getBusinessRulesMethod;
        protected Func<Task<IEnumerable<IRule>>> _getBusinessRulesAsyncMethod;

        public ServiceCommand(Action initializationMethod,
                              Func<Task> initializationAsyncMethod,
                              Func<IEnumerable<ValidationResult>> getErrorsMethod,
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod,
                              Func<IEnumerable<IRule>> getRulesMethod,
                              Func<Task<IEnumerable<IRule>>> getRulesAsyncMethod,
                              Action executeMethod,
                              Func<Task> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
            _getBusinessRulesMethod = getRulesMethod;
            _getBusinessRulesAsyncMethod = getRulesAsyncMethod;
        }

        public ServiceCommand(Action initializationMethod,
                              Func<Task> initializationAsyncMethod,
                              Func<IEnumerable<ValidationResult>> getErrorsMethod,
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod,
                              Action executeMethod,
                              Func<Task> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        public ServiceCommand(Action initializationMethod, Func<Task> initializationAsyncMethod, Action executeMethod, Func<Task> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Action initializationMethod, Action executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task> executeAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Func<IEnumerable<ValidationResult>> getErrorsMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod, Action executeMethod, Func<Task> executeAsyncMethod)
        {
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Action executeMethod, Func<Task> executeAsyncMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Action executeMethod, Func<IEnumerable<ValidationResult>> getErrorsMethod)
        {
            _executeMethod = executeMethod;
            _getErrorsMethod = getErrorsMethod;
        }

        public ServiceCommand(Func<Task> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        public ServiceCommand(Action executeMethod, Func<Task> executeAsyncMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        public ServiceCommand(Action executeMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        public ServiceCommand(Func<Task> executeAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Func<Task> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        protected override void OnInitialization()
        {
            (_initializationMethod ?? base.OnInitialization)();
        }

        protected override void OnExecute()
        {
            (_executeMethod ?? base.OnExecute)();
        }

        protected override async Task OnInitializationAsync()
        {
            await (_initializationAsyncMethod ?? base.OnInitializationAsync)();
        }

        protected async override Task OnExecuteAsync()
        {
            await (_executeAsyncMethod ?? base.OnExecuteAsync)();
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return (_getErrorsMethod ?? base.GetErrors)();
        }

        public override Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return (_getErrorsAsyncMethod ?? base.GetErrorsAsync)();
        }

        protected override IEnumerable<IRule> OnGetRules()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRules)();
        }

        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return (_getBusinessRulesAsyncMethod ?? base.OnGetRulesAsync)();
        }
    }

    public class ServiceCommand<T> : Command<T>
    {
        private Action _initializationMethod;
        private Func<Task> _initializationAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getErrorsMethod;
        private Func<Task<IEnumerable<ValidationResult>>> _getErrorsAsyncMethod;
        private Func<T> _executeMethod;
        private Func<Task<T>> _executeAsyncMethod;
        protected Func<IEnumerable<IRule>> _getBusinessRulesMethod;
        protected Func<Task<IEnumerable<IRule>>> _getBusinessRulesAsyncMethod;

        public ServiceCommand(Action initializationMethod,
                              Func<Task> initializationAsyncMethod,
                              Func<IEnumerable<ValidationResult>> getErrorsMethod,
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod,
                              Func<IEnumerable<IRule>> getBusinessRulesMethod,
                              Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod,
                              Func<T> executeMethod,
                              Func<Task<T>> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        public ServiceCommand(Action initializationMethod,
                              Func<Task> initializationAsyncMethod,
                              Func<IEnumerable<ValidationResult>> getErrorsMethod,
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod,
                              Func<T> executeMethod,
                              Func<Task<T>> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        public ServiceCommand(Action initializationMethod, Func<Task> initializationAsyncMethod, Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Action initializationMethod, Func<T> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task<T>> executeAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Func<IEnumerable<ValidationResult>> getErrorsMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod, Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
        {
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Func<T> executeMethod, Func<IEnumerable<ValidationResult>> getErrorsMethod)
        {
            _executeMethod = executeMethod;
            _getErrorsMethod = getErrorsMethod;
        }

        public ServiceCommand(Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        public ServiceCommand(Func<T> executeMethod, Func<Task<T>> executeAsyncMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        public ServiceCommand(Func<T> executeMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        public ServiceCommand(Func<Task<T>> executeAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
        }

        public ServiceCommand(Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        protected override void OnInitialization()
        {
            (_initializationMethod ?? base.OnInitialization)();
        }

        protected override async Task OnInitializationAsync()
        {
            await (_initializationAsyncMethod ?? base.OnInitializationAsync)();
        }

        protected override T OnExecute()
        {
            return (_executeMethod ?? base.OnExecute)();
        }

        protected override Task<T> OnExecuteAsync()
        {
            return (_executeAsyncMethod ?? base.OnExecuteAsync)();
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return (_getErrorsMethod ?? base.GetErrors)();
        }

        public override Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return (_getErrorsAsyncMethod ?? base.OnGetErrorsAsync)();
        }

        protected override IEnumerable<IRule> OnGetRules()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRules)();
        }

        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return (_getBusinessRulesAsyncMethod ?? base.OnGetRulesAsync)();
        }
    }
}