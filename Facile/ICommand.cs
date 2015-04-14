using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Facile
{
    public interface IValidationErrorsContainer
    {
        IEnumerable<ValidationResult> GetValidationErrors();
    }

    public interface ICommandBase : IValidationErrorsContainer
    {
        bool CanExecute { get; }
    }

    public interface ICommand : ICommandBase
    {
        ExecutionResult Execute();
        Task<ExecutionResult> ExecuteAsync();
    }

    public interface ICommand<T> : ICommandBase 
    {
        ExecutionResult<T> Execute();
        Task<ExecutionResult<T>> ExecuteAsync();
    }
}
