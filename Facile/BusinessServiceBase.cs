using Facile;
using Facile.Exception;
using Facile.Extensions;
using Facile.Rules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facile
{
    /// <summary>
    /// This class will contain a rules collection, potentially dirty state tracking, concurrency checks, etc.
    /// </summary>
    public abstract class BusinessServiceBase<T> : IBusinessService<T>, ITransactionSupportStatusContainer where T : IDomainObject, new()
    {
        private IDataProxy<T> _dataProxy;

        protected IDataProxy<T> DataProxy
        {
            get { return _dataProxy; }
        }

        public BusinessServiceBase(IDataProxy<T> dataProxy)
        {
            _dataProxy = dataProxy;
        }

        protected virtual IEnumerable<IRule> GetBusinessRulesForRetrieve(int id)
        {
            return Enumerable.Empty<IRule>();
        }

        protected virtual IEnumerable<IRule> GetBusinessRulesForInsert(T entity)
        {
            return Enumerable.Empty<IRule>();
        }

        protected virtual IEnumerable<IRule> GetBusinessRulesForUpdate(T entity)
        {
            return Enumerable.Empty<IRule>();
        }

        protected virtual IEnumerable<IRule> GetBusinessRulesForDelete(int id)
        {
            return Enumerable.Empty<IRule>();
        }

        protected virtual IEnumerable<ValidationResult> GetBusinessRulesResults(IEnumerable<IRule> businessRules)
        {
            string entityName = typeof(T).Name;

            var invalidRules = businessRules.ToArray()
                                            .ForEach(rule => rule.Validate())
                                            .Where(rule => !rule.IsValid);

            foreach (var rule in invalidRules)
            {
                yield return new ValidationResult(rule.ErrorMessage, new string[] {  entityName });
            }
        }

        protected virtual IEnumerable<ValidationResult> GetValidationResultsForInsert(T entity)
        {
            foreach (var error in entity.GetValidationErrors())
                yield return error;
        }

        protected virtual IEnumerable<ValidationResult> GetValidationResultsForUpdate(T entity)
        {
            foreach (var error in entity.GetValidationErrors())
                yield return error;
        }

        protected virtual IEnumerable<ValidationResult> GetValidationResultsForDelete(int id)
        {
            if (id <= 0)
                yield return new ValidationResult("id must be greater than 0", new string[] { typeof(T).Name });
        }

        protected virtual IEnumerable<ValidationResult> GetValidationResultsForGetByID(int id)
        {
            if (id <= 0)
                yield return new ValidationResult("id must be greater than 0", new string[] { typeof(T).Name });
        }

        protected virtual IEnumerable<ValidationResult> GetValidationResultsForGetAll()
        {
            yield break;
        }

        public virtual ICommand<T> GetByIDCommand(int id)
        {
            return new ServiceCommand<T>
            (
                executeMethod: () => GetByID(id),
                executeAsyncMethod: () => GetByIDAsync(id),
                getValidationResultsMethod: () => GetValidationResultsForGetByID(id),
                getBusinessRulesResultsMethod: () => GetBusinessRulesResults(GetBusinessRulesForRetrieve(id))
            );
        }

        public virtual ICommand<IEnumerable<T>> GetAllCommand()
        {
            return new ServiceCommand<IEnumerable<T>>
            (
                executeMethod: () => GetAll(),
                executeAsyncMethod: () => GetAllAsync(),
                getValidationResultsMethod: () => GetValidationResultsForGetAll(),
                getBusinessRulesResultsMethod: () => GetBusinessRulesResults(new IRule[] { })
            );
        }

        public virtual ICommand<T> InsertCommand(T entity)
        {
            return new ServiceCommand<T>
            (
                executeMethod: () => Insert(entity),
                executeAsyncMethod: () => InsertAsync(entity),
                getValidationResultsMethod: () => GetValidationResultsForInsert(entity),
                getBusinessRulesResultsMethod: () => GetBusinessRulesResults(GetBusinessRulesForInsert(entity))
            );
        }

        public virtual ICommand<T> UpdateCommand(T entity)
        {
            return new ServiceCommand<T>
            (
                executeMethod: () => Update(entity),
                executeAsyncMethod: () => UpdateAsync(entity),
                getValidationResultsMethod: () => GetValidationResultsForUpdate(entity),
                getBusinessRulesResultsMethod: () => GetBusinessRulesResults(GetBusinessRulesForUpdate(entity))
            );
        }

        public virtual ICommand DeleteCommand(int id)
        {
            return new ServiceCommand
            (
                executeMethod: () => Delete(id),
                executeAsyncMethod: () => DeleteAsync(id),
                getValidationResultsMethod: () => GetValidationResultsForDelete(id),
                getBusinessRulesResultsMethod: () => GetBusinessRulesResults(GetBusinessRulesForDelete(id))
            );
        }

        protected virtual IEnumerable<T> GetAll()
        {
            return _dataProxy.GetAll();
        }

        protected virtual T GetByID(int id)
        {
            return _dataProxy.GetByID(id);
        }

        protected virtual T Insert(T entity)
        {
            return _dataProxy.Insert(entity);
        }

        /// <exception cref="Facile.Exception.DomainObjectNotFoundException" />
        /// <exception cref="Facile.Exception.ConcurrencyException" />
        protected virtual T Update(T entity)
        {
            // only perform this if we're not latency prone - keep it close to the server, no need to do this more than once
            if (this.SupportsTransactions)
            {
                T current = GetByID(entity.ID);
                if (current == null)
                {
                    throw new DomainObjectNotFoundException(BuildNotFoundError(entity.ID));
                }
                entity.RevertNonEditableValues(current);
                entity.RevertForeignKeysFromZeroToNull();
            }
            return _dataProxy.Update(entity);
        }

        protected virtual void Delete(int id)
        {
            _dataProxy.Delete(id);
        }

        protected virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataProxy.GetAllAsync();
        }

        protected virtual async Task<T> GetByIDAsync(int id)
        {
            return await _dataProxy.GetByIDAsync(id);
        }

        protected virtual async Task<T> InsertAsync(T entity)
        {
            return await _dataProxy.InsertAsync(entity);
        }

        protected virtual async Task<T> UpdateAsync(T entity)
        {
            // only perform this if we're not latency prone - keep it close to the server, no need to do this more than once
            if (this.SupportsTransactions)
            {
                T current = await GetByIDAsync(entity.ID);
                if (current == null)
                {
                    throw new DomainObjectNotFoundException(BuildNotFoundError(entity.ID));
                }
                entity.RevertNonEditableValues(current);
                entity.RevertForeignKeysFromZeroToNull();
            }

            return await _dataProxy.UpdateAsync(entity);
        }

        protected virtual Task DeleteAsync(int id)
        {
            return _dataProxy.DeleteAsync(id);
        }

        public bool SupportsTransactions
        {
            get { return _dataProxy.SupportsTransactions; }
        }

        public string BuildNotFoundError(int id)
        {            
            var message = string.Format("{0} ID {1} could not be found.", new T().ClassName(), id.ToString());
            return message;
        }
    }
}
