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
        protected Func<Task> _initializationMethod;

        /// <summary>
        /// </summary>
        protected Func<Task<IEnumerable<ValidationResult>>> _validationMethod;

        /// <summary>
        /// </summary>
        protected Func<Task> _executeMethod;

        /// <summary>
        /// </summary>
        protected Func<Task<IEnumerable<IRule>>> _getBusinessRulesMethod;

        /// <summary>
        /// </summary>
        public ServiceCommand(
            Func<Task> initializationMethod,
            Func<Task<IEnumerable<IRule>>> getRulesMethod,
            Func<Task<IEnumerable<ValidationResult>>> validationMethod,
            Func<Task> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
            _getBusinessRulesMethod = getRulesMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(
            Func<Task> initializationMethod,
            Func<Task<IEnumerable<ValidationResult>>> validationMethod,
            Func<Task> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(
            Func<Task> initializationMethod,
            Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod,
            Func<Task> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> initializationMethod, Func<Task> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task<IEnumerable<ValidationResult>>> validationMethod, Func<Task> executeMethod)
        {
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod, Func<Task> executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(
            Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod,
            Func<Task<IEnumerable<ValidationResult>>> validationMethod,
            Func<Task> executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        protected override async Task OnInitializationAsync()
        {
            await (_initializationMethod ?? base.OnInitializationAsync)();
        }

        /// <summary>
        /// </summary>
        protected override async Task OnExecuteAsync()
        {
            await (_executeMethod ?? base.OnExecuteAsync)();
        }

        /// <summary>
        /// </summary>
        protected override Task<IEnumerable<ValidationResult>> OnValidateAsync()
        {
            return (_validationMethod ?? base.OnValidateAsync)();
        }

        /// <summary>
        /// </summary>
        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRulesAsync)();
        }
    }

    /// <summary>
    /// </summary>
    public class ServiceCommand<T> : Command<T>
    {
        private Func<Task> _initializationMethod;
        private Func<Task<IEnumerable<ValidationResult>>> _validationMethod;
        private Func<Task<T>> _executeMethod;
        private Func<Task<IEnumerable<IRule>>> _getBusinessRulesMethod;

        /// <summary>
        /// </summary>
        public ServiceCommand(
            Func<Task> initializationMethod,
            Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod,
            Func<Task<IEnumerable<ValidationResult>>> validationMethod,
            Func<Task<T>> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(
            Func<Task> initializationMethod,
            Func<Task<IEnumerable<ValidationResult>>> validationMethod,
            Func<Task<T>> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(
            Func<Task> initializationMethod,
            Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod,
            Func<Task<T>> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task> initializationMethod, Func<Task<T>> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task<T>> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task<IEnumerable<ValidationResult>>> validationMethod, Func<Task<T>> executeMethod)
        {
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod, Func<Task<T>> executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public ServiceCommand(
            Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod,
            Func<Task<IEnumerable<ValidationResult>>> validationMethod,
            Func<Task<T>> executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        protected override async Task OnInitializationAsync()
        {
            await (_initializationMethod ?? base.OnInitializationAsync)();
        }

        /// <summary>
        /// </summary>
        protected override Task<T> OnExecuteAsync()
        {
            return (_executeMethod ?? base.OnExecuteAsync)();
        }

        /// <summary>
        /// </summary>
        protected override Task<IEnumerable<ValidationResult>> OnValidateAsync()
        {
            return (_validationMethod ?? base.OnValidateAsync)();
        }

        /// <summary>
        /// </summary>
        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRulesAsync)();
        }
    }
}