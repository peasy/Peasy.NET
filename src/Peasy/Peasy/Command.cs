using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Defines a base command responsible for the execution of a logical unit of code
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// Synchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        public virtual ExecutionResult Execute()
        {
            OnInitialization();

            var validationResults = GetErrors().ToArray();

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

        /// <summary>
        /// Asynchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        public virtual async Task<ExecutionResult> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = (await GetErrorsAsync()).ToArray();

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
        /// Invoked synchronously before rule execution
        /// </summary>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Invoked asynchronously before rule execution
        /// </summary>
        protected virtual Task OnInitializationAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns validation results as a result of rule executions
        /// </summary>
        protected virtual IEnumerable<ValidationResult> OnGetErrors()
        {
            return GetRules().GetValidationResults();
        }

        /// <summary>
        /// Returns validation results as a result of rule executions
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            var errors = await GetRulesAsync();
            return await errors.GetValidationResultsAsync();
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
        protected virtual IEnumerable<IRule> GetRules()
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of rules to be executed via the asynchronous command execution pipeline
        /// </summary>
        /// <remarks>
        /// Override this method to supply custom business rules to execute
        /// </remarks>
        protected virtual Task<IEnumerable<IRule>> GetRulesAsync()
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Synchronously returns a list of validation results as a result of rule execution(s)
        /// </summary>
        public virtual IEnumerable<ValidationResult> GetErrors()
        {
            return OnGetErrors();
        }

        /// <summary>
        /// Asynchronously returns a list of validation results as a result of rule execution(s)
        /// </summary>
        public virtual Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return OnGetErrorsAsync();
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
    }

    /// <summary>
    /// Defines a base command responsible for the execution of a logical unit of code
    /// </summary>
    public abstract class Command<T> : ICommand<T>
    {
        /// <summary>
        /// Synchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        public virtual ExecutionResult<T> Execute()
        {
            OnInitialization();

            var validationResults = GetErrors().ToArray();

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

        /// <summary>
        /// Asynchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        public virtual async Task<ExecutionResult<T>> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = (await GetErrorsAsync()).ToArray();

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
        /// Invoked synchronously before rule execution
        /// </summary>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Invoked asynchronously before rule execution
        /// </summary>
        protected virtual Task OnInitializationAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns validation results as a result of rule executions
        /// </summary>
        protected virtual IEnumerable<ValidationResult> OnGetErrors()
        {
            return GetRules().GetValidationResults();
        }

        /// <summary>
        /// Returns validation results as a result of rule executions
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            var errors = await GetRulesAsync();
            return await errors.GetValidationResultsAsync();
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
        protected virtual IEnumerable<IRule> GetRules()
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of rules to be executed via the asynchronous command execution pipeline
        /// </summary>
        /// <remarks>
        /// Override this method to supply custom business rules to execute
        /// </remarks>
        protected virtual Task<IEnumerable<IRule>> GetRulesAsync()
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Synchronously returns a list of validation results as a result of rule execution(s)
        /// </summary>
        public virtual IEnumerable<ValidationResult> GetErrors()
        {
            return OnGetErrors();
        }

        /// <summary>
        /// Asynchronously returns a list of validation results as a result of rule execution(s)
        /// </summary>
        public virtual Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return OnGetErrorsAsync();
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
    }
}
