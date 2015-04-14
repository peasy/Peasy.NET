using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile
{
    public abstract class ReturnsBoolCommandBase : CommandBase, IReturnsBoolCommand
    {
        public ReturnsBoolExecutionResult Execute()
        {
            var validationResults = GetValidationErrors().ToArray();
            if (validationResults.Any())
                return new ReturnsBoolExecutionResult() { Success = false, Errors = validationResults };

            var result = OnExecute();

            return new ReturnsBoolExecutionResult() { Success = true, Value = result };
        }

        protected abstract bool OnExecute();

        public async Task<ReturnsBoolExecutionResult> ExecuteAsync()
        {
            var validationResults = GetValidationErrors().ToArray();
            if (validationResults.Any())
                return new ReturnsBoolExecutionResult() { Success = false, Errors = validationResults };

            var result = await OnExecuteAsync();

            return new ReturnsBoolExecutionResult() { Success = true, Value = result };
        }

        protected abstract Task<bool> OnExecuteAsync();
    }
}
