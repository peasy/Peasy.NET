using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile
{
    public abstract class ReturnsVoidCommandBase : CommandBase, IReturnsVoidCommand
    {
        public ExecutionResult Execute()
        {
            var validationResults = GetValidationErrors().ToArray();
            if (validationResults.Any())
                return new ExecutionResult() { Success = false, Errors = validationResults };

            OnExecute();

            return new ExecutionResult() { Success = true };
        }

        protected abstract void OnExecute();

        public async Task<ExecutionResult> ExecuteAsync()
        {
            var validationResults = GetValidationErrors().ToArray();
            if (validationResults.Any())
                return new ExecutionResult() { Success = false, Errors = validationResults };

            await OnExecuteAsync();

            return new ExecutionResult() { Success = true };
        }

        protected abstract Task OnExecuteAsync();
    }
}
