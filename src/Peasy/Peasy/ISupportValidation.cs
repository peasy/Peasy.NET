using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface ISupportValidation
    {
        ///
        Task<IValidationOperation<ExecutionResult>> ValidateAsync();
    }

    /// <summary>
    /// </summary>
    public interface ISupportValidation<T>
    {
        ///
        Task<IValidationOperation<ExecutionResult<T>>> ValidateAsync();
    }
}