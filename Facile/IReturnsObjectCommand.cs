using System.Threading.Tasks;

namespace Facile
{
    public interface IReturnsObjectCommand<T> : ICommand
    {
        ReturnsObjectExecutionResult<T> Execute();
        Task<ReturnsObjectExecutionResult<T>> ExecuteAsync();
    }
}
