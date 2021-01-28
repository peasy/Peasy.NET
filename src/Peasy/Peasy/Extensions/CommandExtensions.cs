using System;
using System.Threading.Tasks;
using Peasy.Synchronous;

namespace Peasy.Extensions
{
    ///
    public static class CommandExtensions
    {
        ///
        public static async Task<IValidationOperation<ExecutionResult>> ValidateAsync(this ICommand command)
        {
            var validationSupporter = command as ISupportValidation;
            if (validationSupporter == null) throw new InvalidCastException("Supplied ICommand does not implement ISupportValidation interface.");
            var validationResult = await validationSupporter.ValidateAsync();
            return validationResult;
        }

        ///
        public static async Task<IValidationOperation<ExecutionResult<T>>> ValidateAsync<T>(this ICommand<T> command)
        {
            var validationSupporter = command as ISupportValidation<T>;
            if (validationSupporter == null) throw new InvalidCastException("Supplied ICommand does not implement ISupportValidation<T> interface.");
            var validationResult = await validationSupporter.ValidateAsync();
            return validationResult;
        }

        ///
        public static ISynchronousValidationOperation<ExecutionResult> Validate(this ISynchronousCommand command)
        {
            var validationSupporter = command as ISupportSynchronousValidation;
            if (validationSupporter == null) throw new InvalidCastException("Supplied ICommand does not implement ISupportValidation interface.");
            var validationResult = validationSupporter.Validate();
            return validationResult;
        }

        ///
        public static ISynchronousValidationOperation<ExecutionResult<T>> Validate<T>(this ISynchronousCommand<T> command)
        {
            var validationSupporter = command as ISupportSynchronousValidation<T>;
            if (validationSupporter == null) throw new InvalidCastException("Supplied ICommand does not implement ISupportValidation<T> interface.");
            var validationResult = validationSupporter.Validate();
            return validationResult;
        }
    }
}