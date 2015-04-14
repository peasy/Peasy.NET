using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
namespace Facile
{
    public interface IReturnsBoolCommand : ICommand
    {
        ReturnsBoolExecutionResult Execute();
        Task<ReturnsBoolExecutionResult> ExecuteAsync();
    }
}
