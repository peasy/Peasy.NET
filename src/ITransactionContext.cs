using System;
using System.Threading.Tasks;

namespace Peasy
{
    public interface ITransactionContext
    {
        void Execute(Action codeToRunWithinTransaction);
        T Execute<T>(Func<T> codeToRunWithinTransaction);
        Task<T> ExecuteAsync<T>(Func<Task<T>> codeToRunWithinTransaction);
        Task ExecuteAsync(Func<Task> codeToRunWithinTransaction);
    }
}
