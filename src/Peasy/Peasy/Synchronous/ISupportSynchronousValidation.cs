namespace Peasy.Synchronous
{
    /// <summary>
    /// </summary>
    public interface ISupportSynchronousValidation
    {
        ///
        ISynchronousValidationOperation<ExecutionResult> Validate();
    }

    /// <summary>
    /// </summary>
    public interface ISupportSynchronousValidation<T>
    {
        ///
        ISynchronousValidationOperation<ExecutionResult<T>> Validate();
    }
}