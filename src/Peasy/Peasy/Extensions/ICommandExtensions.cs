using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Peasy.Synchronous;

namespace Peasy.Extensions
{
    /// <summary>
    /// An extensions class used to perform common tasks against type <see cref="ICommand"/>.
    /// </summary>
    public static class ICommandExtensions
    {
        /// <inheritdoc cref="ISupportCommandValidation.ValidateAsync"/>
        /// <exception cref="System.InvalidCastException">Thrown when supplied command does not implement <see cref="ISupportCommandValidation"/>.</exception>
        public static async Task<ICommandValidationResult> ValidateAsync(this ICommand command)
        {
            var validationSupporter = command as ISupportCommandValidation;
            if (validationSupporter == null) throw new InvalidCastException($"Supplied command does not implement {nameof(ISupportCommandValidation)} interface.");
            var validationResult = await validationSupporter.ValidateAsync();
            return validationResult;
        }

        /// <inheritdoc cref="ISupportCommandValidation{T}.ValidateAsync"/>
        /// <exception cref="System.InvalidCastException">Thrown when supplied command does not implement <see cref="ISupportCommandValidation{T}"/>.</exception>
        public static async Task<ICommandValidationResult<T>> ValidateAsync<T>(this ICommand<T> command)
        {
            var validationSupporter = command as ISupportCommandValidation<T>;
            if (validationSupporter == null) throw new InvalidCastException($"Supplied command does not implement {nameof(ISupportCommandValidation<T>)} interface.");
            var validationResult = await validationSupporter.ValidateAsync();
            return validationResult;
        }

        /// <inheritdoc cref="ISupportSynchronousCommandValidation.Validate"/>
        /// <exception cref="System.InvalidCastException">Thrown when supplied command does not implement <see cref="ISupportSynchronousCommandValidation"/>.</exception>
        public static ISynchronousCommandValidationResult Validate(this ISynchronousCommand command)
        {
            var validationSupporter = command as ISupportSynchronousCommandValidation;
            if (validationSupporter == null) throw new InvalidCastException($"Supplied command does not implement {nameof(ISupportSynchronousCommandValidation)} interface.");
            var validationResult = validationSupporter.Validate();
            return validationResult;
        }

        /// <inheritdoc cref="ISupportSynchronousCommandValidation{T}.Validate"/>
        /// <exception cref="System.InvalidCastException">Thrown when supplied command does not implement <see cref="ISupportSynchronousCommandValidation{T}"/>.</exception>
        public static ISynchronousCommandValidationResult<T> Validate<T>(this ISynchronousCommand<T> command)
        {
            var validationSupporter = command as ISupportSynchronousCommandValidation<T>;
            if (validationSupporter == null) throw new InvalidCastException($"Supplied command does not implement {nameof(ISupportSynchronousCommandValidation<T>)} interface.");
            var validationResult = validationSupporter.Validate();
            return validationResult;
        }

        /// <inheritdoc cref="IRulesContainer.GetRulesAsync"/>
        /// <exception cref="System.InvalidCastException">Thrown when supplied command does not implement <see cref="IRulesContainer"/>.</exception>
        public static Task<IEnumerable<IRule>> GetRulesAsync(this ICommand command)
        {
            return GetRulesAsync(command as IRulesContainer);
        }

        /// <inheritdoc cref="IRulesContainer.GetRulesAsync"/>
        /// <exception cref="System.InvalidCastException">Thrown when supplied command does not implement <see cref="IRulesContainer"/>.</exception>
        public static Task<IEnumerable<IRule>> GetRulesAsync<T>(this ICommand<T> command)
        {
            return GetRulesAsync(command as IRulesContainer);
        }

        /// <inheritdoc cref="ISynchronousRulesContainer.GetRules"/>
        /// <exception cref="System.InvalidCastException">Thrown when supplied command does not implement <see cref="ISynchronousRulesContainer"/>.</exception>
        public static IEnumerable<ISynchronousRule> GetRules(this ISynchronousCommand command)
        {
            return GetRules(command as ISynchronousRulesContainer);
        }

        /// <inheritdoc cref="ISynchronousRulesContainer.GetRules"/>
        /// <exception cref="System.InvalidCastException">Thrown when supplied command does not implement <see cref="ISynchronousRulesContainer"/>.</exception>
        public static IEnumerable<ISynchronousRule> GetRules<T>(this ISynchronousCommand<T> command)
        {
            return GetRules(command as ISynchronousRulesContainer);
        }

        private static Task<IEnumerable<IRule>> GetRulesAsync(this IRulesContainer rulesContainer)
        {
            if (rulesContainer == null) throw new InvalidCastException($"Supplied command does not implement {nameof(IRulesContainer)} interface.");
            return rulesContainer.GetRulesAsync();
        }

        private static IEnumerable<ISynchronousRule> GetRules(this ISynchronousRulesContainer rulesContainer)
        {
            if (rulesContainer == null) throw new InvalidCastException($"Supplied command does not implement {nameof(ISynchronousRulesContainer)} interface.");
            return rulesContainer.GetRules();
        }
    }
}