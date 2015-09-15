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
