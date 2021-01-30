using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Synchronous
{
    /// <summary>
    /// A command built for convenience, allows the quick creation of a command without having to create a new concrete command class.
    /// </summary>
    public class SynchronousServiceCommand : SynchronousCommand
    {
        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="SynchronousCommand.Execute"/></remarks>
        protected Action _initializationMethod;

        /// <summary>
        /// Performs rule validations.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="SynchronousCommand.Execute"/>.</remarks>
        /// <returns>A potential list of errors resulting from rule executions.</returns>
        protected Func<IEnumerable<ValidationResult>> _validationMethod;

        /// <summary>
        /// Executes application logic.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="SynchronousCommand.Execute"/> if all rule validations are successful.</remarks>
        protected Action _executeMethod;

        /// <summary>
        /// Composes a list of business and validation rules to execute.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="SynchronousCommand.Execute"/>.</remarks>
        /// <returns>A list of business and validation rules.</returns>
        protected Func<IEnumerable<ISynchronousRule>> _getBusinessRulesMethod;

        /// <summary>
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
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
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="getBusinessRulesMethod">Composes a list of business and validation rules to execute.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
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
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public SynchronousServiceCommand(Action initializationMethod, Action executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public SynchronousServiceCommand(Action executeMethod)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public SynchronousServiceCommand(Func<IEnumerable<ValidationResult>> validationMethod, Action executeMethod)
        {
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="getBusinessRulesMethod">Composes a list of business and validation rules to execute.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public SynchronousServiceCommand(Func<IEnumerable<ISynchronousRule>> getBusinessRulesMethod, Action executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <inheritdoc cref="SynchronousCommand.OnInitialization"/>
        protected override void OnInitialization()
        {
            (_initializationMethod ?? base.OnInitialization)();
        }

        /// <inheritdoc cref="SynchronousCommand.OnExecute"/>
        protected override void OnExecute()
        {
            (_executeMethod ?? base.OnExecute)();
        }

        /// <inheritdoc cref="SynchronousCommand.OnValidate"/>
        protected override IEnumerable<ValidationResult> OnValidate()
        {
            return (_validationMethod ?? base.OnValidate)();
        }

        /// <inheritdoc cref="SynchronousCommand.OnGetRules"/>
        protected override IEnumerable<ISynchronousRule> OnGetRules()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRules)();
        }
    }

    /// <summary>
    /// A command built for convenience, allows the quick creation of a command without having to create a new concrete command class.
    /// </summary>
    public class SynchronousServiceCommand<T> : SynchronousCommand<T>
    {
        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="SynchronousCommand{T}.Execute"/></remarks>
        protected Action _initializationMethod;

        /// <summary>
        /// Performs rule validations.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="SynchronousCommand{T}.Execute"/>.</remarks>
        /// <returns>A potential list of errors resulting from rule executions.</returns>
        protected Func<IEnumerable<ValidationResult>> _validationMethod;

        /// <summary>
        /// Executes application logic.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="SynchronousCommand{T}.Execute"/> if all rule validations are successful.</remarks>
        protected Func<T> _executeMethod;

        /// <summary>
        /// Composes a list of business and validation rules to execute.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="SynchronousCommand{T}.Execute"/>.</remarks>
        /// <returns>A list of business and validation rules.</returns>
        protected Func<IEnumerable<ISynchronousRule>> _getBusinessRulesMethod;

        /// <summary>
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
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
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="getBusinessRulesMethod">Composes a list of business and validation rules to execute.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
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
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public SynchronousServiceCommand(Action initializationMethod, Func<T> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public SynchronousServiceCommand(Func<T> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public SynchronousServiceCommand(Func<IEnumerable<ValidationResult>> validationMethod, Func<T> executeMethod)
        {
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// Initializes a new instance of the SynchronousServiceCommand.
        /// </summary>
        /// <param name="getBusinessRulesMethod">Composes a list of business and validation rules to execute.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public SynchronousServiceCommand(Func<IEnumerable<ISynchronousRule>> getBusinessRulesMethod, Func<T> executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <inheritdoc cref="SynchronousCommand{T}.OnInitialization"/>
        protected override void OnInitialization()
        {
            (_initializationMethod ?? base.OnInitialization)();
        }

        /// <inheritdoc cref="SynchronousCommand{T}.OnExecute"/>
        protected override T OnExecute()
        {
            return (_executeMethod ?? base.OnExecute)();
        }

        /// <inheritdoc cref="SynchronousCommand{T}.OnValidate"/>
        protected override IEnumerable<ValidationResult> OnValidate()
        {
            return (_validationMethod ?? base.OnValidate)();
        }

        /// <inheritdoc cref="SynchronousCommand{T}.OnGetRules"/>
        protected override IEnumerable<ISynchronousRule> OnGetRules()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRules)();
        }
    }
}