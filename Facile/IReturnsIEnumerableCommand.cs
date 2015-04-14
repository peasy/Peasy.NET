using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facile
{
    public interface IReturnsIEnumerableCommand<T> : ICommand
    {
        ReturnsIEnumerableExecutionResult<T> Execute(bool yieldAllRules = true);
        Task<ReturnsIEnumerableExecutionResult<T>> ExecuteAsync();
    }
}
