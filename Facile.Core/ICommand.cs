using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Facile.Core
{
    public interface IValidationErrorsContainer
    {
        IEnumerable<ValidationResult> GetErrors();
        Task<IEnumerable<ValidationResult>> GetErrorsAsync();
    }

    public interface ICommand : IValidationErrorsContainer
    {
        ExecutionResult Execute();
        Task<ExecutionResult> ExecuteAsync();
    }

    public interface ICommand<T> : IValidationErrorsContainer 
    {
        ExecutionResult<T> Execute();
        Task<ExecutionResult<T>> ExecuteAsync();
    }
}
