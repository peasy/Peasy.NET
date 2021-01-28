using System;
using System.Threading.Tasks;
using Peasy.Synchronous;

namespace Peasy.Extensions
{
    ///
    public static class CommandExtensions
    {
        ///
        public static async Task<ICommandValidationResult<ExecutionResult>> ValidateAsync(this ICommand command)
        {
            var validationSupporter = command as ISupportCommandValidation;
            if (validationSupporter == null) throw new InvalidCastException("Supplied ICommand does not implement ISupportValidation interface.");
            var validationResult = await validationSupporter.ValidateAsync();
            return validationResult;
        }

        ///
        public static async Task<ICommandValidationResult<ExecutionResult<T>>> ValidateAsync<T>(this ICommand<T> command)
        {
            var validationSupporter = command as ISupportCommandValidation<T>;
            if (validationSupporter == null) throw new InvalidCastException("Supplied ICommand does not implement ISupportValidation<T> interface.");
            var validationResult = await validationSupporter.ValidateAsync();
            return validationResult;
        }

        ///
        public static ISynchronousCommandValidationResult<ExecutionResult> Validate(this ISynchronousCommand command)
        {
            var validationSupporter = command as ISupportSynchronousCommandValidation;
            if (validationSupporter == null) throw new InvalidCastException("Supplied ICommand does not implement ISupportValidation interface.");
            var validationResult = validationSupporter.Validate();
            return validationResult;
        }

        ///
        public static ISynchronousCommandValidationResult<ExecutionResult<T>> Validate<T>(this ISynchronousCommand<T> command)
        {
            var validationSupporter = command as ISupportSynchronousCommandValidation<T>;
            if (validationSupporter == null) throw new InvalidCastException("Supplied ICommand does not implement ISupportValidation<T> interface.");
            var validationResult = validationSupporter.Validate();
            return validationResult;
        }
    }
}