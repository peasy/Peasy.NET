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
        public ExecutionResult Execute()
        {
            OnInitialization();

            var validationResults = GetErrors().ToArray();
            if (validationResults.Any())
                return new ExecutionResult { Success = false, Errors = validationResults };

            try
            {
                OnExecute();
            }
            catch (ServiceException ex)
            {
                return new ExecutionResult { Success = false, Errors = new ValidationResult[] { new ValidationResult(ex.Message) }};
            }

            return new ExecutionResult { Success = true };
        }

        /// <summary>
        /// Asynchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        public async Task<ExecutionResult> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = await GetErrorsAsync();
            if (validationResults.ToArray().Any())
                return new ExecutionResult { Success = false, Errors = validationResults };

            try
            {
                await OnExecuteAsync();
            }
            catch (ServiceException ex)
            {
                return new ExecutionResult { Success = false, Errors = new ValidationResult[] { new ValidationResult(ex.Message) }};
            }

            return new ExecutionResult { Success = true };
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

        public virtual IEnumerable<ValidationResult> GetErrors()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public virtual Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return Task.FromResult(Enumerable.Empty<ValidationResult>());
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
        public ExecutionResult<T> Execute()
        {
            OnInitialization();

            var validationResults = GetErrors().ToArray();
            if (validationResults.Any())
                return new ExecutionResult<T> { Success = false, Errors = validationResults };

            T result;
            try
            {
                result = OnExecute();
            }
            catch (ServiceException ex)
            {
                return new ExecutionResult<T> { Success = false, Errors = new ValidationResult[] { new ValidationResult(ex.Message) }};
            }

            return new ExecutionResult<T> { Success = true, Value = result };
        }

        /// <summary>
        /// Asynchronously orchestrates command initialization, rule execution, and command execution
        /// </summary>
        public async Task<ExecutionResult<T>> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = await GetErrorsAsync();
            if (validationResults.ToArray().Any())
                return new ExecutionResult<T> { Success = false, Errors = validationResults };

            T result;
            try
            {
                result = await OnExecuteAsync();
            }
            catch (ServiceException ex)
            {
                return new ExecutionResult<T> { Success = false, Errors = new ValidationResult[] { new ValidationResult(ex.Message) }};
            }

            return new ExecutionResult<T> { Success = true, Value = result };
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

        public virtual IEnumerable<ValidationResult> GetErrors()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public virtual Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return Task.FromResult(Enumerable.Empty<ValidationResult>());
        }
    }
}
