using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Core
{
    public abstract class Command : CommandBase, ICommand
    {
        public ExecutionResult Execute()
        //public ExecutionResult Execute(bool yieldAllRules = true)
        {
            OnInitialization();

            var validationResults = GetErrors().ToArray();
            if (validationResults.Any())
                return new ExecutionResult() { Success = false, Errors = validationResults };

            OnExecute();

            return new ExecutionResult() { Success = true };
        }

        protected virtual void OnInitialization() { }

        protected abstract void OnExecute();

        public async Task<ExecutionResult> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = GetErrors().ToArray();
            if (validationResults.Any())
                return new ExecutionResult() { Success = false, Errors = validationResults };

            await OnExecuteAsync();

            return new ExecutionResult() { Success = true };
        }

        protected virtual async Task OnInitializationAsync()
        {
            await Task.Yield();
            //await Task.Run(() => { });
        }

        protected abstract Task OnExecuteAsync();
    }

    public abstract class Command<T> : CommandBase, ICommand<T>
    {
        //public ExecutionResult<T> Execute(bool yieldAllRules = true)
        public ExecutionResult<T> Execute()
        {
            OnInitialization();

            var validationResults = GetErrors().ToArray();
            if (validationResults.Any())
                return new ExecutionResult<T>() { Success = false, Errors = validationResults };

            var result = OnExecute();

            return new ExecutionResult<T>() { Success = true, Value = result };
        }

        protected virtual void OnInitialization() { }

        protected abstract T OnExecute();

        public async Task<ExecutionResult<T>> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = GetErrors().ToArray();
            if (validationResults.Any())
                return new ExecutionResult<T>() { Success = false, Errors = validationResults };

            var result = await OnExecuteAsync();

            return new ExecutionResult<T>() { Success = true, Value = result };
        }

        protected virtual async Task OnInitializationAsync()
        {
            await Task.Yield();
            //await Task.Run(() => { });
        }

        protected abstract Task<T> OnExecuteAsync();
    }
}
