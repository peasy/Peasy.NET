using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
namespace Facile
{
    public interface IReturnsVoidCommand : ICommand
    {
        ExecutionResult Execute();
        Task<ExecutionResult> ExecuteAsync();
    }
}
