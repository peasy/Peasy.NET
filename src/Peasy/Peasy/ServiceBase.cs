using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Peasy.Extensions;

namespace Peasy
{
    /// <summary>
    /// Base class of all business services
    /// </summary>
    /// <typeparam name="T">Any type that implements <see cref="IDomainObject"/>.</typeparam>
    /// <typeparam name="TKey">Any type.</typeparam>
    /// <typeparam></typeparam>
    public abstract class ServiceBase<T, TKey> : IService<T, TKey> where T : IDomainObject<TKey>, new()
    {
        /// <summary>
        /// Represents the data abstraction that
        /// </summary>
        protected readonly IDataProxy<T, TKey> _dataProxy;

        /// <summary>
        /// Represents the data abstraction that
        /// </summary>
        protected IDataProxy<T, TKey> DataProxy => _dataProxy;

        /// <summary>
        /// Represents the data abstraction that
        /// </summary>
        public ServiceBase(IDataProxy<T, TKey> dataProxy)
        {
            _dataProxy = dataProxy;
        }

        /// <summary>
        /// The first method invoked within the execution pipleline of the command returned by <see cref="GetByIDCommand"/>
        /// </summary>
        /// <remarks>
        /// Override this method to perform initialization logic before rule validations for GetByIDCommand.Execute are performed
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">A shared state bag available to all methods within the execution of the command pipeline</param>
        protected virtual void OnGetByIDCommandInitialization(TKey id, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for GetByIDCommand.ExecuteAsync are performed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task OnGetByIDCommandInitializationAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for GetAllCommand.Execute are performed
        /// </summary>
        /// <param name="context"></param>
        protected virtual void OnGetAllCommandInitialization(ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for GetAllCommand.ExecuteAsync are performed
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task OnGetAllCommandInitializationAsync(ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for InsertCommand.Execute are performed
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="context"></param>
        protected virtual void OnInsertCommandInitialization(T resource, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for InsertCommand.ExecuteAsync are performed
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task OnInsertCommandInitializationAsync(T entity, ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for UpdateCommand.Execute are performed
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="context"></param>
        protected virtual void OnUpdateCommandInitialization(T entity, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for UpdateCommand.ExecuteAsync are performed
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task OnUpdateCommandInitializationAsync(T entity, ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for DeleteCommand.Execute are performed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        protected virtual void OnDeleteCommandInitialization(TKey id, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Override this method to perform initialization logic before rule validations for DeleteCommand.ExecuteAsync are performed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task OnDeleteCommandInitializationAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// The second method that executes in the command pipeline returned by <see cref="GetByIDCommand"/>, whose successful execution will determine whether to continue or not. 
        /// </summary>
        /// <remarks>
        /// Override this method to perform initialization logic before rule validations for GetByIDCommand.Execute are performed
        /// </remarks>
        /// <param name="id">The id of the resource.</param>
        /// <param name="context">A shared state bag passed through the execution of the command pipeline</param>
        /// <summary>
        /// Override this method to supply custom business rules to GetByIDCommand.Execute()
        /// </summary>
        /// <returns>A list of rules</returns>
        protected virtual IEnumerable<IRule> GetBusinessRulesForGetByID(TKey id, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Override this method to supply custom business rules to GetByIDCommand.ExecuteAsync()
        /// </summary>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForGetByIDAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Override this method to supply custom business rules to GetAllCommand.Execute()
        /// </summary>
        protected virtual IEnumerable<IRule> GetBusinessRulesForGetAll(ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Override this method to supply custom business rules to GetByIDCommand.ExecuteAsync()
        /// </summary>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForGetAllAsync(ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Override this method to supply custom business rules to InsertCommand.Execute()
        /// </summary>
        protected virtual IEnumerable<IRule> GetBusinessRulesForInsert(T entity, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Override this method to supply custom business rules to InsertCommand.ExecuteAsync()
        /// </summary>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForInsertAsync(T entity, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Override this method to supply custom business rules to UpdateCommand.Execute()
        /// </summary>
        protected virtual IEnumerable<IRule> GetBusinessRulesForUpdate(T entity, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Override this method to supply custom business rules to UpdateCommand.ExecuteAsync()
        /// </summary>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForUpdateAsync(T entity, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Override this method to supply custom business rules to DeleteCommand.Execute()
        /// </summary>
        protected virtual IEnumerable<IRule> GetBusinessRulesForDelete(TKey id, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Builds validation results from the supplied list of IRule
        /// </summary>
        /// <remarks>
        /// Override this method to supply custom business rules to DeleteCommand.ExecuteAsync()
        /// </remarks>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForDeleteAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// The third and final method that executes in the command pipeline returned by <see cref="GetByIDCommand"/>, only executes upon successful validation of rules
        /// </summary>
        /// <remarks>
        /// Override this method to perform initialization logic before rule validations for GetByIDCommand.Execute are performed
        /// </remarks>
        /// <summary>
        /// Invoked by GetByIDCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual T GetByID(TKey id, ExecutionContext<T> context)
        {
            return _dataProxy.GetByID(id);
        }

        /// <summary>
        /// Invoked by GetByIDCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual async Task<T> GetByIDAsync(TKey id, ExecutionContext<T> context)
        {
            return await _dataProxy.GetByIDAsync(id);
        }

        /// <summary>
        /// Invoked by GetAllCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual IEnumerable<T> GetAll(ExecutionContext<T> context)
        {
            return _dataProxy.GetAll();
        }

        /// <summary>
        /// Invoked by GetAllCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual async Task<IEnumerable<T>> GetAllAsync(ExecutionContext<T> context)
        {
            return await _dataProxy.GetAllAsync();
        }

        /// <summary>
        /// Invoked by InsertCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual T Insert(T entity, ExecutionContext<T> context)
        {
            return _dataProxy.Insert(entity);
        }

        /// <summary>
        /// Invoked by InsertCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual async Task<T> InsertAsync(T entity, ExecutionContext<T> context)
        {
            return await _dataProxy.InsertAsync(entity);
        }

        /// <summary>
        /// Invoked by UpdateCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual T Update(T entity, ExecutionContext<T> context)
        {
            return _dataProxy.Update(entity);
        }

        /// <summary>
        /// Invoked by UpdateCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual async Task<T> UpdateAsync(T entity, ExecutionContext<T> context)
        {
            return await _dataProxy.UpdateAsync(entity);
        }

        /// <summary>
        /// Invoked by DeleteCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual void Delete(TKey id, ExecutionContext<T> context)
        {
            _dataProxy.Delete(id);
        }

        /// <summary>
        /// Invoked by DeleteCommand() if validation and business rules execute successfully
        /// </summary>
        protected virtual Task DeleteAsync(TKey id, ExecutionContext<T> context)
        {
            return _dataProxy.DeleteAsync(id);
        }

        /// <summary>
        /// Invoked by <see cref="GetAllErrorsForGetByID" /> and <see cref="GetAllErrorsForGetByIDAsync" />, generates a potential list of <see cref="ValidationResult"/> based on the supplied resource id.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForGetByID(TKey id, ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Invoked by <see cref="GetAllErrorsForGetByID" /> and <see cref="GetAllErrorsForGetByIDAsync" />, generates a potential list of <see cref="ValidationResult"/> based on the supplied resource id.
        /// </summary>
        /// <remarks>
        /// Invoked by <see cref="GetAllErrorsForGetAll" /> and <see cref="GetAllErrorsForGetAllAsync" />, generates a potential list of <see cref="ValidationResult"/>.
        /// Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForGetAll(ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Invoked by <see cref="GetAllErrorsForInsert" /> and <see cref="GetAllErrorsForGetByIDAsync" />, generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForInsert(T resource, ExecutionContext<T> context)
        {
            return resource.GetValidationErrors();
        }

        /// <summary>
        /// Invoked by <see cref="GetAllErrorsForUpdate" /> and <see cref="GetAllErrorsForUpdateAsync" />, generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForUpdate(T resource, ExecutionContext<T> context)
        {
            return resource.GetValidationErrors();
        }

        /// <summary>
        /// Invoked by <see cref="GetAllErrorsForDelete" /> and <see cref="GetAllErrorsForDeleteAsync" />, generates a potential list of <see cref="ValidationResult"/> based on the supplied resource id.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForDelete(TKey id, ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual IEnumerable<ValidationResult> GetAllErrorsForGetByID(TKey id, ExecutionContext<T> context)
        {
            return GetValidationResultsForGetByID(id, context).Concat(GetBusinessRulesForGetByID(id, context).GetValidationResults());
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> GetAllErrorsForGetByIDAsync(TKey id, ExecutionContext<T> context)
        {
            var rules = await GetBusinessRulesForGetByIDAsync(id, context);
            return GetValidationResultsForGetByID(id, context).Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// This method is invoked by the command returned by <see cref="GetAllCommand"/>.
        /// </summary>
        /// <remarks>
        /// Override this method to manipulate error construction.  Example: You might want to verify that no validation errors exist before invoking potentially expensive business rules method.
        /// </remarks>
        /// <summary>
        /// Invoked by <see cref="GetAllErrorsForGetAll" /> and <see cref="GetAllErrorsForGetAllAsync" />, generates a potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetAllErrorsForGetAll(ExecutionContext<T> context)
        {
            return GetValidationResultsForGetAll(context).Concat(GetBusinessRulesForGetAll(context).GetValidationResults());
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> GetAllErrorsForGetAllAsync(ExecutionContext<T> context)
        {
            var rules = await GetBusinessRulesForGetAllAsync(context);
            return GetValidationResultsForGetAll(context).Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual IEnumerable<ValidationResult> GetAllErrorsForInsert(T entity, ExecutionContext<T> context)
        {
            var validationErrors = GetValidationResultsForInsert(entity, context);
            var businessRuleErrors = GetBusinessRulesForInsert(entity, context).GetValidationResults();
            return validationErrors.Concat(businessRuleErrors);
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> GetAllErrorsForInsertAsync(T entity, ExecutionContext<T> context)
        {
            var validationErrors = GetValidationResultsForInsert(entity, context);
            var rules = await GetBusinessRulesForInsertAsync(entity, context);
            return validationErrors.Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual IEnumerable<ValidationResult> GetAllErrorsForUpdate(T entity, ExecutionContext<T> context)
        {
            var validationErrors = GetValidationResultsForUpdate(entity, context);
            var businessRuleErrors = GetBusinessRulesForUpdate(entity, context).GetValidationResults();
            return validationErrors.Concat(businessRuleErrors);
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> GetAllErrorsForUpdateAsync(T entity, ExecutionContext<T> context)
        {
            var validationErrors = GetValidationResultsForUpdate(entity, context);
            var rules = await GetBusinessRulesForUpdateAsync(entity, context);
            return validationErrors.Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual IEnumerable<ValidationResult> GetAllErrorsForDelete(TKey id, ExecutionContext<T> context)
        {
            return GetValidationResultsForDelete(id, context).Concat(GetBusinessRulesForDelete(id, context).GetValidationResults());
        }

        /// <summary>
        /// Override this method to manipulate error construction.  Ie, you may want to verify that no validation errors exist before invoking potentially expensive business rules method
        /// </summary>
        protected virtual async Task<IEnumerable<ValidationResult>> GetAllErrorsForDeleteAsync(TKey id, ExecutionContext<T> context)
        {
            var rules = await GetBusinessRulesForDeleteAsync(id, context);
            return GetValidationResultsForDelete(id, context).Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Composes a <see cref="T:Peasy.ICommand`1" /> that invokes <see cref="T:Peasy.IDataProxy`1" />.GetByID() upon successful execution of business and validation rules
        /// </summary>
        public virtual ICommand<T> GetByIDCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnGetByIDCommandInitialization(id, context),
                initializationAsyncMethod: () => OnGetByIDCommandInitializationAsync(id, context),
                getErrorsMethod: () => GetAllErrorsForGetByID(id, context),
                getErrorsAsyncMethod: () => GetAllErrorsForGetByIDAsync(id, context),
                executeMethod: () => GetByID(id, context),
                executeAsyncMethod: () => GetByIDAsync(id, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="T:Peasy.ICommand`1" /> that invokes <see cref="T:Peasy.IDataProxy`1" />.GetAll() upon successful execution of business and validation rules
        /// </summary>
        public virtual ICommand<IEnumerable<T>> GetAllCommand()
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<IEnumerable<T>>
            (
                initializationMethod: () => OnGetAllCommandInitialization(context),
                initializationAsyncMethod: () => OnGetAllCommandInitializationAsync(context),
                getErrorsMethod: () => GetAllErrorsForGetAll(context),
                getErrorsAsyncMethod: () => GetAllErrorsForGetAllAsync(context),
                executeMethod: () => GetAll(context),
                executeAsyncMethod: () => GetAllAsync(context)
            );
        }

        /// <summary>
        /// Composes a <see cref="T:Peasy.ICommand`1" /> that invokes <see cref="T:Peasy.IDataProxy`1" />.Insert() upon successful execution of business and validation rules
        /// </summary>
        public virtual ICommand<T> InsertCommand(T entity)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnInsertCommandInitialization(entity, context),
                initializationAsyncMethod: () => OnInsertCommandInitializationAsync(entity, context),
                getErrorsMethod: () => GetAllErrorsForInsert(entity, context),
                getErrorsAsyncMethod: () => GetAllErrorsForInsertAsync(entity, context),
                executeMethod: () => Insert(entity, context),
                executeAsyncMethod: () => InsertAsync(entity, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="T:Peasy.ICommand`1" /> that invokes <see cref="T:Peasy.IDataProxy`1" />.Update() upon successful execution of business and validation rules
        /// </summary>
        /// <param name="entity"></param>
        public virtual ICommand<T> UpdateCommand(T entity)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnUpdateCommandInitialization(entity, context),
                initializationAsyncMethod: () => OnUpdateCommandInitializationAsync(entity, context),
                getErrorsMethod: () => GetAllErrorsForUpdate(entity, context),
                getErrorsAsyncMethod: () => GetAllErrorsForUpdateAsync(entity, context),
                executeMethod: () => Update(entity, context),
                executeAsyncMethod: () => UpdateAsync(entity, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Peasy.ICommand" /> that invokes <see cref="T:Peasy.IDataProxy`1" />.Delete() upon successful execution of business and validation rules
        /// </summary>
        public virtual ICommand DeleteCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand
            (
                initializationMethod: () => OnDeleteCommandInitialization(id, context),
                initializationAsyncMethod: () => OnDeleteCommandInitializationAsync(id, context),
                getErrorsMethod: () => GetAllErrorsForDelete(id, context),
                getErrorsAsyncMethod: () => GetAllErrorsForDeleteAsync(id, context),
                executeMethod: () => Delete(id, context),
                executeAsyncMethod: () => DeleteAsync(id, context)
            );
        }

    }
}
