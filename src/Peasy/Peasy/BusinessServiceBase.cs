using Peasy.Exception;
using Peasy.Extensions;
using Peasy.Rules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Base class of all business services
    /// </summary>
    public abstract class BusinessServiceBase<T, TKey> : ServiceBase<T, TKey> where T : IDomainObject<TKey>, new()
    {
        /// <summary>
        /// </summary>
        protected BusinessServiceBase(IServiceDataProxy<T, TKey> dataProxy) : base(dataProxy)
        {
        }

        /// <summary>
        /// Supplies validation results to DeleteCommand()
        /// </summary>
        protected override IEnumerable<ValidationResult> GetValidationResultsForDelete(TKey id, ExecutionContext<T> context)
        {
            var rule = id.CreateValueRequiredRule(nameof(id)).Validate();
            if (!rule.IsValid)
                yield return new ValidationResult(rule.ErrorMessage, new string[] { typeof(T).Name });
        }

        /// <summary>
        /// Supplies validation results to GetByIDCommand()
        /// </summary>
        protected override IEnumerable<ValidationResult> GetValidationResultsForGetByID(TKey id, ExecutionContext<T> context)
        {
            var rule = id.CreateValueRequiredRule(nameof(id)).Validate();
            if (!rule.IsValid)
                yield return new ValidationResult(rule.ErrorMessage, new string[] { typeof(T).Name });
        }

        /// <summary>
        /// Invoked by UpdateCommand() if validation and business rules execute successfully
        /// </summary>
        /// <exception cref="Peasy.Exception.DomainObjectNotFoundException" />
        /// <exception cref="Peasy.Exception.ConcurrencyException" />
        protected override T Update(T entity, ExecutionContext<T> context)
        {
            // only perform this if we're not latency prone - keep it close to the server, no need to do this more than once
            if (!IsLatencyProne)
            {
                var current = context.CurrentEntity ?? this.GetByID(entity.ID, context);

                if (current == null)
                {
                    throw new DomainObjectNotFoundException(BuildNotFoundError(entity.ID));
                }

                if (current is IVersionContainer container)
                {
                    var rule = new ConcurrencyCheckRule(container, entity as IVersionContainer).Validate();
                    if (!rule.IsValid)
                        throw new ConcurrencyException(rule.ErrorMessage);
                }

                entity.RevertNonEditableValues(current);
                entity.RevertForeignKeysFromZeroToNull();
            }
            return _dataProxy.Update(entity);
        }

        /// <summary>
        /// Invoked by UpdateCommand() if validation and business rules execute successfully
        /// </summary>
        protected override async Task<T> UpdateAsync(T entity, ExecutionContext<T> context)
        {
            // only perform this if we're not latency prone - keep it close to the server, no need to do this more than once
            if (!IsLatencyProne)
            {
                var current = context.CurrentEntity ?? await this.GetByIDAsync(entity.ID, context);

                if (current == null)
                {
                    throw new DomainObjectNotFoundException(BuildNotFoundError(entity.ID));
                }
                if (current is IVersionContainer container)
                {
                    var rule = new ConcurrencyCheckRule(container, entity as IVersionContainer).Validate();
                    if (!rule.IsValid)
                        throw new ConcurrencyException(rule.ErrorMessage);

                }
                entity.RevertNonEditableValues(current);
                entity.RevertForeignKeysFromZeroToNull();
            }

            return await _dataProxy.UpdateAsync(entity);
        }

        /// <summary>
        /// </summary>
        public bool SupportsTransactions => (_dataProxy as IServiceDataProxy<T, TKey>).SupportsTransactions;

        /// <summary>
        /// </summary>
        public bool IsLatencyProne => (_dataProxy as IServiceDataProxy<T, TKey>).IsLatencyProne;

        /// <summary>
        /// </summary>
        protected string BuildNotFoundError(TKey id)
        {
            var message = $"{new T().ClassName()} ID {id.ToString()} could not be found.";
            return message;
        }
    }
}
