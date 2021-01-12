using System;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface ITransactionContext
    {
        /// <summary>
        /// </summary>
        void Execute(Action codeToRunWithinTransaction);

        /// <summary>
        /// </summary>
        T Execute<T>(Func<T> codeToRunWithinTransaction);

        /// <summary>
        /// </summary>
        Task<T> ExecuteAsync<T>(Func<Task<T>> codeToRunWithinTransaction);

        /// <summary>
        /// </summary>
        Task ExecuteAsync(Func<Task> codeToRunWithinTransaction);
    }
}
