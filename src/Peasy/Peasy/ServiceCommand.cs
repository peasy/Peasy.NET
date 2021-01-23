using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public class ServiceCommand : Command
    {
        /// <summary>
        /// </summary>
        protected Action _initializationMethod;

        /// <summary>
        /// </summary>
        protected Func<Task> _initializationAsyncMethod;

        /// <summary>
        /// </summary>
        protected Func<IEnumerable<ValidationResult>> _getErrorsMethod;

        /// <summary>
        /// </summary>
        protected Func<Task<IEnumerable<ValidationResult>>> _getErrorsAsyncMethod;

        /// <summary>
        /// </summary>
        protected Action _executeMethod;

        /// <summary>
        /// </summary>
        protected Func<Task> _executeAsyncMethod;

        /// <summary>
        /// </summary>
        protected Func<IEnumerable<IRule>> _getBusinessRulesMethod;

        /// <summary>
        /// </summary>
        protected Func<Task<IEnumerable<IRule>>> _getBusinessRulesAsyncMethod;

        /// <summary>
        /// </summary>
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

        /// <summary>
        /// </summary>
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

        /// <summary>
        /// </summary>
        public ServiceCommand(Action initializationMethod, Func<Task> initializationAsyncMethod, Action executeMethod, Func<Task> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Action initializationMethod, Action executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task> executeAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<IEnumerable<ValidationResult>> getErrorsMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod, Action executeMethod, Func<Task> executeAsyncMethod)
        {
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Action executeMethod, Func<Task> executeAsyncMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Action executeMethod, Func<IEnumerable<ValidationResult>> getErrorsMethod)
        {
            _executeMethod = executeMethod;
            _getErrorsMethod = getErrorsMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Action executeMethod, Func<Task> executeAsyncMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Action executeMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> executeAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        /// <summary>
        /// </summary>
        protected override void OnInitialization()
        {
            (_initializationMethod ?? base.OnInitialization)();
        }

        /// <summary>
        /// </summary>
        protected override void OnExecute()
        {
            (_executeMethod ?? base.OnExecute)();
        }

        /// <summary>
        /// </summary>
        protected override async Task OnInitializationAsync()
        {
            await (_initializationAsyncMethod ?? base.OnInitializationAsync)();
        }

        /// <summary>
        /// </summary>
        protected override async Task OnExecuteAsync()
        {
            await (_executeAsyncMethod ?? base.OnExecuteAsync)();
        }

        /// <summary>
        /// </summary>
        protected override IEnumerable<ValidationResult> OnGetErrors()
        {
            return (_getErrorsMethod ?? base.OnGetErrors)();
        }

        /// <summary>
        /// </summary>
        protected override Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            return (_getErrorsAsyncMethod ?? base.OnGetErrorsAsync)();
        }

        /// <summary>
        /// </summary>
        protected override IEnumerable<IRule> OnGetRules()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRules)();
        }

        /// <summary>
        /// </summary>
        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return (_getBusinessRulesAsyncMethod ?? base.OnGetRulesAsync)();
        }
    }

    /// <summary>
    /// </summary>
    public class ServiceCommand<T> : Command<T>
    {
        private Action _initializationMethod;
        private Func<Task> _initializationAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getErrorsMethod;
        private Func<Task<IEnumerable<ValidationResult>>> _getErrorsAsyncMethod;
        private Func<T> _executeMethod;
        private Func<Task<T>> _executeAsyncMethod;

        /// <summary>
        /// </summary>
        protected Func<IEnumerable<IRule>> _getBusinessRulesMethod;

        /// <summary>
        /// </summary>
        protected Func<Task<IEnumerable<IRule>>> _getBusinessRulesAsyncMethod;

        /// <summary>
        /// </summary>
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

        /// <summary>
        /// </summary>
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

        /// <summary>
        /// </summary>
        public ServiceCommand(Action initializationMethod, Func<Task> initializationAsyncMethod, Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Action initializationMethod, Func<T> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task<T>> executeAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<IEnumerable<ValidationResult>> getErrorsMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod, Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
        {
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<T> executeMethod, Func<IEnumerable<ValidationResult>> getErrorsMethod)
        {
            _executeMethod = executeMethod;
            _getErrorsMethod = getErrorsMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<T> executeMethod, Func<Task<T>> executeAsyncMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<T> executeMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task<T>> executeAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getBusinessRulesAsyncMethod = getBusinessRulesAsyncMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
        {
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        /// <summary>
        /// </summary>
        protected override void OnInitialization()
        {
            (_initializationMethod ?? base.OnInitialization)();
        }

        /// <summary>
        /// </summary>
        protected override async Task OnInitializationAsync()
        {
            await (_initializationAsyncMethod ?? base.OnInitializationAsync)();
        }

        /// <summary>
        /// </summary>
        protected override T OnExecute()
        {
            return (_executeMethod ?? base.OnExecute)();
        }

        /// <summary>
        /// </summary>
        protected override Task<T> OnExecuteAsync()
        {
            return (_executeAsyncMethod ?? base.OnExecuteAsync)();
        }

        /// <summary>
        /// </summary>
        protected override IEnumerable<ValidationResult> OnGetErrors()
        {
            return (_getErrorsMethod ?? base.OnGetErrors)();
        }

        /// <summary>
        /// </summary>
        protected override Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            return (_getErrorsAsyncMethod ?? base.OnGetErrorsAsync)();
        }

        /// <summary>
        /// </summary>
        protected override IEnumerable<IRule> OnGetRules()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRules)();
        }

        /// <summary>
        /// </summary>
        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return (_getBusinessRulesAsyncMethod ?? base.OnGetRulesAsync)();
        }
    }
}