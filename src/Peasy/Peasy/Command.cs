using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Defines a base command responsible for the execution of a logical unit of code
    /// </summary>
    public abstract class Command : ICommand, IRulesContainer, IValidationErrorsContainer
    {
        ///<inheritdoc cref="ICommand.Execute"/>
        public virtual ExecutionResult Execute()
        {
            OnInitialization();

            var validationResults = OnGetErrors().ToArray();

            if (validationResults.Any()) return OnFailedExecution(validationResults);

            try
            {
                OnExecute();
            }
            catch (ServiceException ex)
            {
                return OnServiceException(ex);
            }

            return OnSuccessfulExecution();
        }

        ///<inheritdoc cref="ICommand.ExecuteAsync"/>
        public virtual async Task<ExecutionResult> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = (await OnGetErrorsAsync()).ToArray();

            if (validationResults.Any()) return OnFailedExecution(validationResults);

            try
            {
                await OnExecuteAsync();
            }
            catch (ServiceException ex)
            {
                return OnServiceException(ex);
            }

            return OnSuccessfulExecution();
        }

        /// <summary>
        /// Invoked synchronously before rule execution.
        /// </summary>
        /// <remarks>Override this method to perform invoke initialization logic before rule executions occur.</remarks>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Invoked asynchronously before rule execution.
        /// </summary>
        /// <remarks>Override this method to perform invoke initialization logic before rule executions occur.</remarks>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        protected virtual Task OnInitializationAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns validation results as a result of rule executions
        /// </summary>
        protected virtual IEnumerable<ValidationResult> OnGetErrors()
        {
            return OnGetRules().GetValidationResults();
        }

        /// <summary>
        /// Returns validation results as a result of rule executions
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            var rules = await OnGetRulesAsync();
            return await rules.GetValidationResultsAsync();
        }

        /// <summary>
        /// Invoked synchronously if rule executions are successful
        /// </summary>
        protected virtual void OnExecute() { }

        /// <summary>
        /// Invoked asynchronously if rule executions are successful
        /// </summary>
        protected virtual Task OnExecuteAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns a list of rules to be executed via the synchronous command execution pipeline
        /// </summary>
        /// <remarks>
        /// Override this method to supply custom business rules to execute
        /// </remarks>
        protected virtual IEnumerable<IRule> OnGetRules()
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of rules to be executed via the asynchronous command execution pipeline
        /// </summary>
        /// <remarks>
        /// Override this method to supply custom business rules to execute
        /// </remarks>
        protected virtual Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Invoked when an exception of type Peasy.ServiceException is caught
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom ValidationResult or to further manipulate the ValidationResult
        /// </remarks>
        protected virtual ExecutionResult OnServiceException(ServiceException exception)
        {
            return OnFailedExecution(new[] { new ValidationResult(exception.Message) });
        }

        /// <summary>
        /// Invoked when any of the rules in the pipeline fail execution
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom ExecutionResult or to further manipulate the ExecutionResult
        /// </remarks>
        protected virtual ExecutionResult OnFailedExecution(IEnumerable<ValidationResult> validationResults)
        {
            return new ExecutionResult { Success = false, Errors = validationResults };
        }

        /// <summary>
        /// Invoked when all of the rules in the pipeline succeed in execution
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom ExecutionResult or to further manipulate the ExecutionResult
        /// </remarks>
        protected virtual ExecutionResult OnSuccessfulExecution()
        {
            return new ExecutionResult { Success = true };
        }

        ///<inheritdoc cref="IValidationErrorsContainer.GetErrors"/>
        public IEnumerable<ValidationResult> GetErrors()
        {
            return OnGetErrors();
        }

        ///<inheritdoc cref="IValidationErrorsContainer.GetErrorsAsync"/>
        public Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return OnGetErrorsAsync();
        }

        ///<inheritdoc cref="IRulesContainer.GetRulesAsync"/>
        public Task<IEnumerable<IRule>> GetRulesAsync()
        {
            return OnGetRulesAsync();
        }

        ///<inheritdoc cref="IRulesContainer.GetRules"/>
        public IEnumerable<IRule> GetRules()
        {
            return OnGetRules();
        }
    }

    /// <summary>
    /// Defines a base command responsible for the execution of a logical unit of code
    /// </summary>
    public abstract class Command<T> : ICommand<T>, IRulesContainer
    {
        ///<inheritdoc cref="ICommand{T}.Execute"/>
        public virtual ExecutionResult<T> Execute()
        {
            OnInitialization();

            var validationResults = OnGetErrors().ToArray();

            if (validationResults.Any()) return OnFailedExecution(validationResults);

            T result;
            try
            {
                result = OnExecute();
            }
            catch (ServiceException ex)
            {
                return OnServiceException(ex);
            }

            return OnSuccessfulExecution(result);
        }

        ///<inheritdoc cref="ICommand{T}.ExecuteAsync"/>
        public virtual async Task<ExecutionResult<T>> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = (await OnGetErrorsAsync()).ToArray();

            if (validationResults.Any()) return OnFailedExecution(validationResults);

            T result;
            try
            {
                result = await OnExecuteAsync();
            }
            catch (ServiceException ex)
            {
                return OnServiceException(ex);
            }

            return OnSuccessfulExecution(result);
        }

        /// <summary>
        /// Invoked synchronously before rule execution.
        /// </summary>
        /// <remarks>Override this method to perform invoke initialization logic before rule executions occur.</remarks>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Invoked asynchronously before rule execution.
        /// </summary>
        /// <remarks>Override this method to perform invoke initialization logic before rule executions occur.</remarks>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        protected virtual Task OnInitializationAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns validation results as a result of rule executions
        /// </summary>
        protected virtual IEnumerable<ValidationResult> OnGetErrors()
        {
            return OnGetRules().GetValidationResults();
        }

        /// <summary>
        /// Returns validation results as a result of rule executions
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            var rules = await OnGetRulesAsync();
            return await rules.GetValidationResultsAsync();
        }

        /// <summary>
        /// Invoked synchronously if rule executions are successful
        /// </summary>
        protected virtual T OnExecute()
        {
            return default(T);
        }

        /// <summary>
        /// Invoked asynchronously if rule executions are successful
        /// </summary>
        protected virtual Task<T> OnExecuteAsync()
        {
            return Task.FromResult(default(T));
        }

        /// <summary>
        /// Returns a list of rules to be executed via the synchronous command execution pipeline
        /// </summary>
        /// <remarks>
        /// Override this method to supply custom business rules to execute
        /// </remarks>
        protected virtual IEnumerable<IRule> OnGetRules()
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of rules to be executed via the asynchronous command execution pipeline
        /// </summary>
        /// <remarks>
        /// Override this method to supply custom business rules to execute
        /// </remarks>
        protected virtual Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Invoked when an exception of type Peasy.ServiceException is caught
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom ValidationResult or to further manipulate the ValidationResult
        /// </remarks>
        protected virtual ExecutionResult<T> OnServiceException(ServiceException exception)
        {
            return OnFailedExecution(new[] { new ValidationResult(exception.Message) });
        }

        /// <summary>
        /// Invoked when any of the rules in the pipeline fail execution
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom ExecutionResult or to further manipulate the ExecutionResult
        /// </remarks>
        protected virtual ExecutionResult<T> OnFailedExecution(ValidationResult[] validationResults)
        {
            return new ExecutionResult<T> { Success = false, Errors = validationResults };
        }

        /// <summary>
        /// Invoked when all of the rules in the pipeline succeed in execution
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom ExecutionResult or to further manipulate the ExecutionResult
        /// </remarks>
        protected virtual ExecutionResult<T> OnSuccessfulExecution(T value)
        {
            return new ExecutionResult<T> { Success = true, Value = value };
        }

        ///<inheritdoc cref="IValidationErrorsContainer.GetErrors"/>
        public IEnumerable<ValidationResult> GetErrors()
        {
            return OnGetErrors();
        }

        ///<inheritdoc cref="IValidationErrorsContainer.GetErrorsAsync"/>
        public Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return OnGetErrorsAsync();
        }

        ///<inheritdoc cref="IRulesContainer.GetRulesAsync"/>
        public Task<IEnumerable<IRule>> GetRulesAsync()
        {
            return OnGetRulesAsync();
        }

        ///<inheritdoc cref="IRulesContainer.GetRules"/>
        public IEnumerable<IRule> GetRules()
        {
            return OnGetRules();
        }
    }
}
