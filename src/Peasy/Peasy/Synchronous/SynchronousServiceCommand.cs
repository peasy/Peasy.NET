using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Synchronous
{
    /// <summary>
    /// </summary>
    public class SynchronousServiceCommand : SynchronousCommand
    {
        /// <summary>
        /// </summary>
        protected Action _initializationMethod;

        /// <summary>
        /// </summary>
        protected Func<IEnumerable<ValidationResult>> _validationMethod;

        /// <summary>
        /// </summary>
        protected Action _executeMethod;

        /// <summary>
        /// </summary>
        protected Func<IEnumerable<ISynchronousRule>> _getBusinessRulesMethod;

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(
            Action initializationMethod,
            Func<IEnumerable<ValidationResult>> validationMethod,
            Action executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(
            Action initializationMethod,
            Func<IEnumerable<ISynchronousRule>> getBusinessRulesMethod,
            Action executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(Action initializationMethod, Action executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(Action executeMethod)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(Func<IEnumerable<ValidationResult>> validationMethod, Action executeMethod)
        {
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(Func<IEnumerable<ISynchronousRule>> getBusinessRulesMethod, Action executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
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
        protected override IEnumerable<ValidationResult> OnValidate()
        {
            return (_validationMethod ?? base.OnValidate)();
        }

        /// <summary>
        /// </summary>
        protected override IEnumerable<ISynchronousRule> OnGetRules()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRules)();
        }
    }

    /// <summary>
    /// </summary>
    public class SynchronousServiceCommand<T> : SynchronousCommand<T>
    {
        private Action _initializationMethod;
        private Func<IEnumerable<ValidationResult>> _validationMethod;
        private Func<T> _executeMethod;
        private Func<IEnumerable<ISynchronousRule>> _getBusinessRulesMethod;

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(
            Action initializationMethod,
            Func<IEnumerable<ValidationResult>> validationMethod,
            Func<T> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(
            Action initializationMethod,
            Func<IEnumerable<ISynchronousRule>> getBusinessRulesMethod,
            Func<T> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(Action initializationMethod, Func<T> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(Func<T> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(Func<IEnumerable<ValidationResult>> validationMethod, Func<T> executeMethod)
        {
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// </summary>
        public SynchronousServiceCommand(Func<IEnumerable<ISynchronousRule>> getBusinessRulesMethod, Func<T> executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <summary>
        /// </summary>
        protected override void OnInitialization()
        {
            (_initializationMethod ?? base.OnInitialization)();
        }

        /// <summary>
        /// </summary>
        protected override T OnExecute()
        {
            return (_executeMethod ?? base.OnExecute)();
        }

        /// <summary>
        /// </summary>
        protected override IEnumerable<ValidationResult> OnValidate()
        {
            return (_validationMethod ?? base.OnValidate)();
        }

        /// <summary>
        /// </summary>
        protected override IEnumerable<ISynchronousRule> OnGetRules()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRules)();
        }
    }
}