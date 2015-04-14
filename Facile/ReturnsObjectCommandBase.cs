using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Facile
{
    public abstract class ReturnsObjectCommandBase<T> : CommandBase, IReturnsObjectCommand<T>
    {
        public ReturnsObjectExecutionResult<T> Execute()
        {
            OnInitialization();

            var validationResults = GetValidationErrors().ToArray();
            if (validationResults.Any())
                return new ReturnsObjectExecutionResult<T>() { Success = false, Errors = validationResults };

            var result = OnExecute();

            return new ReturnsObjectExecutionResult<T>() { Success = true, Value = result };
        }

        protected virtual void OnInitialization() { }

        protected abstract T OnExecute();

        public async Task<ReturnsObjectExecutionResult<T>> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = GetValidationErrors().ToArray();
            if (validationResults.Any())
                return new ReturnsObjectExecutionResult<T>() { Success = false, Errors = validationResults };

            var result = await OnExecuteAsync();

            return new ReturnsObjectExecutionResult<T>() { Success = true, Value = result };
        }

        protected abstract Task<T> OnExecuteAsync();

        protected virtual async Task OnInitializationAsync()
        {
            await Task.Yield();
        }
    }
}

