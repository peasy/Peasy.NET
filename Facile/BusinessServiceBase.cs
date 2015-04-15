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
    /// Base class of all business services
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

        /// <summary>
        /// Override this method to supply custom business rules to GetAllCommand() and GetByIDCommand()
        /// </summary>
        protected virtual IEnumerable<IRule> GetBusinessRulesForRetrieve(int id)
        {
            return Enumerable.Empty<IRule>();
        }
        
        /// <summary>
        /// Override this method to supply custom business rules to InsertCommand()
        /// </summary>
        protected virtual IEnumerable<IRule> GetBusinessRulesForInsert(T entity)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Override this method to supply custom business rules to UpdateCommand()
        /// </summary>
        protected virtual IEnumerable<IRule> GetBusinessRulesForUpdate(T entity)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Override this method to supply custom business rules to DeleteCommand() 
        /// </summary>
        protected virtual IEnumerable<IRule> GetBusinessRulesForDelete(int id)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Builds validation results from the supplied list of IRule
        /// </summary>
        /// <param name="businessRules"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Supplies validation results to InsertCommand()
        /// </summary>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForInsert(T entity)
        {
            foreach (var error in entity.GetValidationErrors())
                yield return error;
        }
        
        /// <summary>
        /// Supplies validation results to UpdateCommand()
        /// </summary>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForUpdate(T entity)
        {
            foreach (var error in entity.GetValidationErrors())
                yield return error;
        }

        /// <summary>
        /// Supplies validation results to DeleteCommand()
        /// </summary>

        protected virtual IEnumerable<ValidationResult> GetValidationResultsForDelete(int id)
        {
            if (id <= 0)
                yield return new ValidationResult("id must be greater than 0", new string[] { typeof(T).Name });
        }
        
        /// <summary>
        /// Supplies validation results to GetByIDCommand()
        /// </summary>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForGetByID(int id)
        {
            if (id <= 0)
                yield return new ValidationResult("id must be greater than 0", new string[] { typeof(T).Name });
        }

        /// <summary>
        /// Supplies validation results to GetAllCommand()
        /// </summary>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForGetAll()
        {
            yield break;
        }

        /// <summary>
        /// Composes a <see cref="T:Facile.ICommand`1" /> that invokes <see cref="T:Facile.IDataProxy`1" />.GetByID() upon successful execution of business and validation rules
        /// </summary>
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

        /// <summary>
        /// Composes a <see cref="T:Facile.ICommand`1" /> that invokes <see cref="T:Facile.IDataProxy`1" />.GetAll() upon successful execution of business and validation rules
        /// </summary>
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

        /// <summary>
        /// Composes a <see cref="T:Facile.ICommand`1" /> that invokes <see cref="T:Facile.IDataProxy`1" />.Insert() upon successful execution of business and validation rules
        /// </summary>
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
        
        /// <summary>
        /// Composes a <see cref="T:Facile.ICommand`1" /> that invokes <see cref="T:Facile.IDataProxy`1" />.Update() upon successful execution of business and validation rules
        /// </summary>
        /// <param name="entity"></param>
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

        /// <summary>
        /// Composes a <see cref="Facile.ICommand" /> that invokes <see cref="T:Facile.IDataProxy`1" />.Delete() upon successful execution of business and validation rules
        /// </summary>
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

        /// <summary>
        /// Invoked by GetAllCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual IEnumerable<T> GetAll()
        {
            return _dataProxy.GetAll();
        }

        /// <summary>
        /// Invoked by GetByIDCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual T GetByID(int id)
        {
            return _dataProxy.GetByID(id);
        }

        /// <summary>
        /// Invoked by InsertCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual T Insert(T entity)
        {
            return _dataProxy.Insert(entity);
        }

        /// <summary>
        /// Invoked by UpdateCommand() if validation and business rules execute successfully
        /// </summary>
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

        /// <summary>
        /// Invoked by DeleteCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual void Delete(int id)
        {
            _dataProxy.Delete(id);
        }

        /// <summary>
        /// Invoked by GetAllCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataProxy.GetAllAsync();
        }

        /// <summary>
        /// Invoked by GetByIDCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual async Task<T> GetByIDAsync(int id)
        {
            return await _dataProxy.GetByIDAsync(id);
        }

        /// <summary>
        /// Invoked by InsertCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual async Task<T> InsertAsync(T entity)
        {
            return await _dataProxy.InsertAsync(entity);
        }

        /// <summary>
        /// Invoked by UpdateCommand() if validation and business rules execute successfully
        /// </summary>
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

        /// <summary>
        /// Invoked by DeleteCommand() if validation and business rules execute successfully
        /// </summary>
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
