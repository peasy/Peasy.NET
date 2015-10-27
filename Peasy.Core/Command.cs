using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy.Core
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

            OnExecute();

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

            await OnExecuteAsync();

            return new ExecutionResult { Success = true };
        }

        /// <summary>
        /// Invoked synchronously if rule executions are successful
        /// </summary>
        protected abstract void OnExecute();

        /// <summary>
        /// Invoked asynchronously if rule executions are successful
        /// </summary>
        protected abstract Task OnExecuteAsync();

        /// <summary>
        /// Invoked synchronously before rule execution
        /// </summary>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Invoked asynchronously before rule execution
        /// </summary>
        protected virtual async Task OnInitializationAsync()
        {
        }

        public virtual IEnumerable<ValidationResult> GetErrors()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public virtual async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return Enumerable.Empty<ValidationResult>();
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

            var result = OnExecute();

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

            var result = await OnExecuteAsync();

            return new ExecutionResult<T> { Success = true, Value = result };
        }

        /// <summary>
        /// Invoked synchronously if rule executions are successful
        /// </summary>
        protected abstract T OnExecute();

        /// <summary>
        /// Invoked asynchronously if rule executions are successful
        /// </summary>
        protected abstract Task<T> OnExecuteAsync();

        /// <summary>
        /// Invoked synchronously before rule execution
        /// </summary>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Invoked asynchronously before rule execution
        /// </summary>
        protected virtual async Task OnInitializationAsync()
        {
        }

        public virtual IEnumerable<ValidationResult> GetErrors()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public virtual async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
