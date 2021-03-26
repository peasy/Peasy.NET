using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// A command built for convenience, allows the quick creation of a command without having to create a new concrete command implementation.
    /// </summary>
    public class ServiceCommand : CommandBase
    {
        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="CommandBase.ExecuteAsync"/></remarks>
        protected Func<Task> _initializationMethod;

        /// <summary>
        /// Performs rule validations.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="CommandBase.ExecuteAsync"/>.</remarks>
        /// <returns>A potential awaitable list of errors resulting from rule executions.</returns>
        protected Func<Task<IEnumerable<ValidationResult>>> _validationMethod;

        /// <summary>
        /// Executes application logic.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="CommandBase.ExecuteAsync"/> if all rule validations are successful.</remarks>
        /// <returns>An awaitable task.</returns>
        protected Func<Task> _executeMethod;

        /// <summary>
        /// Composes a list of business and validation rules to execute.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="CommandBase.ExecuteAsync"/>.</remarks>
        /// <returns>An awaitable list of business and validation rules.</returns>
        protected Func<Task<IEnumerable<IRule>>> _getBusinessRulesMethod;

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="validationMethodUsesTheseRules">
        /// <para>Supplies business rules for the sole purpose of supporting the <see cref="IRulesContainer"/> interface.</para>
        /// <para>NOTE: rules returned from this function will NOT be executed within the command execution pipeline.</para>
        /// </param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(
            Func<Task> initializationMethod,
            Func<Task<IEnumerable<ValidationResult>>> validationMethod,
            Func<Task<IEnumerable<IRule>>> validationMethodUsesTheseRules,
            Func<Task> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _validationMethod = validationMethod;
            _getBusinessRulesMethod = validationMethodUsesTheseRules;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
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
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="getBusinessRulesMethod">Composes a list of business and validation rules to execute.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
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
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(Func<Task> initializationMethod, Func<Task> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(Func<Task> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(Func<Task<IEnumerable<ValidationResult>>> validationMethod, Func<Task> executeMethod)
        {
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="getBusinessRulesMethod">Composes a list of business and validation rules to execute.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod, Func<Task> executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <inheritdoc cref="CommandBase.OnInitializationAsync"/>
        protected override async Task OnInitializationAsync()
        {
            await (_initializationMethod ?? base.OnInitializationAsync)();
        }

        /// <inheritdoc cref="CommandBase.OnExecuteAsync"/>
        protected override async Task OnExecuteAsync()
        {
            await (_executeMethod ?? base.OnExecuteAsync)();
        }

        /// <inheritdoc cref="CommandBase.OnValidateAsync"/>
        protected override Task<IEnumerable<ValidationResult>> OnValidateAsync()
        {
            return (_validationMethod ?? base.OnValidateAsync)();
        }

        /// <inheritdoc cref="CommandBase.OnGetRulesAsync"/>
        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRulesAsync)();
        }
    }

    /// <summary>
    /// A command built for convenience, allows the quick creation of a command without having to create a new concrete command implementation.
    /// </summary>
    public class ServiceCommand<T> : CommandBase<T>
    {
        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="CommandBase{T}.ExecuteAsync"/></remarks>
        protected Func<Task> _initializationMethod;

        /// <summary>
        /// Performs rule validations.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="CommandBase{T}.ExecuteAsync"/>.</remarks>
        /// <returns>A potential awaitable list of errors resulting from rule executions.</returns>
        protected Func<Task<IEnumerable<ValidationResult>>> _validationMethod;

        /// <summary>
        /// Executes application logic.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="CommandBase{T}.ExecuteAsync"/> if all rule validations are successful.</remarks>
        /// <returns>An awaitable task.</returns>
        protected Func<Task<T>> _executeMethod;

        /// <summary>
        /// Composes a list of business and validation rules to execute.
        /// </summary>
        /// <remarks>Invoked within the execution pipeline triggered by <see cref="CommandBase{T}.ExecuteAsync"/>.</remarks>
        /// <returns>An awaitable list of business and validation rules.</returns>
        protected Func<Task<IEnumerable<IRule>>> _getBusinessRulesMethod;

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="validationMethodUsesTheseRules">
        /// <para>Supplies business rules for the sole purpose of supporting the <see cref="IRulesContainer"/> interface.</para>
        /// <para>NOTE: rules returned from this function will NOT be executed within the command execution pipeline.</para>
        /// </param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(
            Func<Task> initializationMethod,
            Func<Task<IEnumerable<ValidationResult>>> validationMethod,
            Func<Task<IEnumerable<IRule>>> validationMethodUsesTheseRules,
            Func<Task<T>> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _validationMethod = validationMethod;
            _getBusinessRulesMethod = validationMethodUsesTheseRules;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
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
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="getBusinessRulesMethod">Composes a list of business and validation rules to execute.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
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
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="initializationMethod">Performs initialization logic before rule validations occur.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(Func<Task> initializationMethod, Func<Task<T>> executeMethod)
        {
            _initializationMethod = initializationMethod;
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(Func<Task<T>> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="validationMethod">Performs rule validations whose result will determine whether command pipeline execution continues.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(Func<Task<IEnumerable<ValidationResult>>> validationMethod, Func<Task<T>> executeMethod)
        {
            _executeMethod = executeMethod;
            _validationMethod = validationMethod;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceCommand.
        /// </summary>
        /// <param name="getBusinessRulesMethod">Composes a list of business and validation rules to execute.</param>
        /// <param name="executeMethod">Executes application logic if all configured rules successfully pass validation.</param>
        public ServiceCommand(Func<Task<IEnumerable<IRule>>> getBusinessRulesMethod, Func<Task<T>> executeMethod)
        {
            _executeMethod = executeMethod;
            _getBusinessRulesMethod = getBusinessRulesMethod;
        }

        /// <inheritdoc cref="CommandBase{T}.OnInitializationAsync"/>
        protected override async Task OnInitializationAsync()
        {
            await (_initializationMethod ?? base.OnInitializationAsync)();
        }

        /// <inheritdoc cref="CommandBase{T}.OnExecuteAsync"/>
        protected override Task<T> OnExecuteAsync()
        {
            return (_executeMethod ?? base.OnExecuteAsync)();
        }

        /// <inheritdoc cref="CommandBase{T}.OnValidateAsync"/>
        protected override Task<IEnumerable<ValidationResult>> OnValidateAsync()
        {
            return (_validationMethod ?? base.OnValidateAsync)();
        }

        /// <inheritdoc cref="CommandBase{T}.OnGetRulesAsync"/>
        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return (_getBusinessRulesMethod ?? base.OnGetRulesAsync)();
        }
    }
}