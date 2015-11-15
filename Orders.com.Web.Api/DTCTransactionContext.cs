using Peasy;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Orders.com.Web.Api 
{
    public class DTCTransactionContext : ITransactionContext
    {
        public void Execute(Action codeToRunWithinTransaction)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 5, 0)))
            {
                codeToRunWithinTransaction();
                transactionScope.Complete();
            }
        }

        public T Execute<T>(Func<T> codeToRunWithinTransaction)
        {
            using (var transactionScope = new TransactionScope())
            {
                var result = codeToRunWithinTransaction();
                transactionScope.Complete();
                return result;
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> codeToRunWithinTransaction)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await codeToRunWithinTransaction();
                transactionScope.Complete();
                return result;
            }
        }

        public async Task ExecuteAsync(Func<Task> codeToRunWithinTransaction)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await codeToRunWithinTransaction();
                transactionScope.Complete();
            }
        }
    }
}
