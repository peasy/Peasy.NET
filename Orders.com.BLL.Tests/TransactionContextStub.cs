using Peasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL.Tests
{
    public class TransactionContextStub : ITransactionContext
    {
        public void Execute(Action codeToRunWithinTransaction)
        {
            codeToRunWithinTransaction();
        }

        public T Execute<T>(Func<T> codeToRunWithinTransaction)
        {
            return codeToRunWithinTransaction();
        }

        public async Task ExecuteAsync(Func<Task> codeToRunWithinTransaction)
        {
            await codeToRunWithinTransaction();
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> codeToRunWithinTransaction)
        {
            return await codeToRunWithinTransaction();
        }
    }
}
