using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoebe.Core.Transaction
{
    public interface ITransactionContext
    {
        void Execute(Action codeToRunWithinTransaction);
        T Execute<T>(Func<T> codeToRunWithinTransaction);
        Task<T> ExecuteAsync<T>(Func<Task<T>> codeToRunWithinTransaction);
        Task ExecuteAsync(Func<Task> codeToRunWithinTransaction);
    }
}
