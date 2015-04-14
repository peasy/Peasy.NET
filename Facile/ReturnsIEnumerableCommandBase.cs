using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Facile
{

    public abstract class ReturnsIEnumerableCommandBase<T> : CommandBase, IReturnsIEnumerableCommand<T>
    {
        public ReturnsIEnumerableExecutionResult<T> Execute(bool yieldAllRules = true)
        {
            OnInitialization();

            var validationResults = GetValidationErrors().ToArray();
            if (validationResults.Any())
                return new ReturnsIEnumerableExecutionResult<T>() { Success = false, Errors = validationResults };

            var result = OnExecute();

            return new ReturnsIEnumerableExecutionResult<T>() { Success = true, Value = result };
        }

        protected virtual void OnInitialization() { }

        protected abstract IEnumerable<T> OnExecute();

        public async Task<ReturnsIEnumerableExecutionResult<T>> ExecuteAsync()
        {
            await OnInitializationAsync();

            var validationResults = GetValidationErrors().ToArray();
            if (validationResults.Any())
                return new ReturnsIEnumerableExecutionResult<T>() { Success = false, Errors = validationResults };

            var result = await OnExecuteAsync();

            return new ReturnsIEnumerableExecutionResult<T>() { Success = true, Value = result };
        }

        protected virtual async Task OnInitializationAsync()
        {
            await Task.Yield();
            //await Task.Run(() => { });
        }

        protected abstract Task<IEnumerable<T>> OnExecuteAsync();
    }
}
