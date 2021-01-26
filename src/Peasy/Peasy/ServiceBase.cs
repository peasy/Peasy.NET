using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Peasy.Extensions;

namespace Peasy
{
    /// <summary>
    /// Serves as the base class of all business services and represents an <see cref="ICommand"/> factory.
    /// </summary>
    /// <typeparam name="T">Any type that implements <see cref="IDomainObject{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Any type.</typeparam>
    public abstract class ServiceBase<T, TKey> : IService<T, TKey> where T : IDomainObject<TKey>, new()
    {
        /// <inheritdoc cref="IDataProxy{T, TKey}"/>
        protected readonly IDataProxy<T, TKey> _dataProxy;

        /// <inheritdoc cref="IDataProxy{T, TKey}"/>
        protected IDataProxy<T, TKey> DataProxy => _dataProxy;

        /// <summary>
        /// Initializes a new service instance with a required data proxy.
        /// </summary>
        /// <param name="dataProxy">The data proxy abstraction to be consumed by commands.</param>
        public ServiceBase(IDataProxy<T, TKey> dataProxy)
        {
            _dataProxy = dataProxy;
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="GetByIDCommand"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        protected virtual void OnGetByIDCommandInitialization(TKey id, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="GetByIDCommand"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        protected virtual Task OnGetByIDCommandInitializationAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="GetAllCommand"/>.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        protected virtual void OnGetAllCommandInitialization(ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="GetAllCommand"/>.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        protected virtual Task OnGetAllCommandInitializationAsync(ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="InsertCommand"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        protected virtual void OnInsertCommandInitialization(T resource, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="InsertCommand"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        protected virtual Task OnInsertCommandInitializationAsync(T resource, ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="UpdateCommand"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        protected virtual void OnUpdateCommandInitialization(T resource, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="UpdateCommand"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        protected virtual Task OnUpdateCommandInitializationAsync(T resource, ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="DeleteCommand"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        protected virtual void OnDeleteCommandInitialization(TKey id, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="DeleteCommand"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        protected virtual Task OnDeleteCommandInitializationAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetByIDCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> GetBusinessRulesForGetByID(TKey id, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetByIDCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForGetByIDAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="GetAllCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetAllCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> GetBusinessRulesForGetAll(ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="GetAllCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetAllCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForGetAllAsync(ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="InsertCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="InsertCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> GetBusinessRulesForInsert(T resource, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="InsertCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="InsertCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForInsertAsync(T resource, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="UpdateCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="UpdateCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> GetBusinessRulesForUpdate(T resource, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="UpdateCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="UpdateCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForUpdateAsync(T resource, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="DeleteCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="DeleteCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> GetBusinessRulesForDelete(TKey id, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="DeleteCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="DeleteCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> GetBusinessRulesForDeleteAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A resource returned from <see cref="Peasy.ISupportGetByID{T, TKey}.GetByID"/> of <see cref="DataProxy"/>.</returns>
        protected virtual T OnGetByIDCommandValidationSuccess(TKey id, ExecutionContext<T> context)
        {
            return _dataProxy.GetByID(id);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>An awaitable resource returned from <see cref="Peasy.ISupportGetByIDAsync{T, TKey}.GetByIDAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual async Task<T> OnGetByIDCommandValidationSuccessAsync(TKey id, ExecutionContext<T> context)
        {
            return await _dataProxy.GetByIDAsync(id);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A resource list returned from <see cref="Peasy.ISupportGetAll{T}.GetAll"/> of <see cref="DataProxy"/>.</returns>
        protected virtual IEnumerable<T> OnGetAllCommandValidationSuccess(ExecutionContext<T> context)
        {
            return _dataProxy.GetAll();
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>An awaitable resource list returned from <see cref="Peasy.ISupportGetAllAsync{T}.GetAllAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual async Task<IEnumerable<T>> OnGetAllCommandValidationSuccessAsync(ExecutionContext<T> context)
        {
            return await _dataProxy.GetAllAsync();
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An updated representation of the resource resulting from a call to <see cref="Peasy.ISupportInsert{T}.Insert"/> of <see cref="DataProxy"/>.</returns>
        protected virtual T OnInsertCommandValidationSuccess(T resource, ExecutionContext<T> context)
        {
            return _dataProxy.Insert(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An awaitable updated representation of the resource resulting from a call to <see cref="Peasy.ISupportInsertAsync{T}.InsertAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual async Task<T> OnInsertCommandValidationSuccessAsync(T resource, ExecutionContext<T> context)
        {
            return await _dataProxy.InsertAsync(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An updated representation of the resource resulting from a call to <see cref="Peasy.ISupportUpdate{T}.Update"/> of <see cref="DataProxy"/>.</returns>
        protected virtual T OnUpdateCommandValidationSuccess(T resource, ExecutionContext<T> context)
        {
            return _dataProxy.Update(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An awaitable updated representation of the resource resulting from a call to <see cref="Peasy.ISupportUpdateAsync{T}.UpdateAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual async Task<T> OnUpdateCommandValidationSuccessAsync(T resource, ExecutionContext<T> context)
        {
            return await _dataProxy.UpdateAsync(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="Command.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns><see cref="void"/> resulting from a call to <see cref="Peasy.ISupportDelete{T}.Delete"/> of <see cref="DataProxy"/>.</returns>
        protected virtual void OnDeleteCommandValidationSuccess(TKey id, ExecutionContext<T> context)
        {
            _dataProxy.Delete(id);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="Command.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>An awaitable <see cref="Task"/> resulting from a call to <see cref="Peasy.ISupportDeleteAsync{T}.DeleteAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual Task OnDeleteCommandValidationSuccessAsync(TKey id, ExecutionContext<T> context)
        {
            return _dataProxy.DeleteAsync(id);
        }

        /// <summary>
        /// Generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnGetByIDCommandValidation"/> and <see cref="OnGetByIDCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForGetByID(TKey id, ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnGetAllCommandValidation"/> and <see cref="OnGetAllCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForGetAll(ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnInsertCommandValidation"/> and <see cref="OnInsertCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForInsert(T resource, ExecutionContext<T> context)
        {
            return resource.Validate();
        }

        /// <summary>
        /// Generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnUpdateCommandValidation"/> and <see cref="OnUpdateCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForUpdate(T resource, ExecutionContext<T> context)
        {
            return resource.Validate();
        }

        /// <summary>
        /// Generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnDeleteCommandValidation"/> and <see cref="OnDeleteCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> GetValidationResultsForDelete(TKey id, ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForGetByID"/> and <see cref="GetBusinessRulesForGetByID"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnGetByIDCommandValidation(TKey id, ExecutionContext<T> context)
        {
            return GetValidationResultsForGetByID(id, context).Concat(GetBusinessRulesForGetByID(id, context).GetValidationResults());
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForGetByID"/> and <see cref="GetBusinessRulesForGetByIDAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnGetByIDCommandValidationAsync(TKey id, ExecutionContext<T> context)
        {
            var rules = await GetBusinessRulesForGetByIDAsync(id, context);
            return GetValidationResultsForGetByID(id, context).Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForGetAll"/> and <see cref="GetBusinessRulesForGetAll"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnGetAllCommandValidation(ExecutionContext<T> context)
        {
            return GetValidationResultsForGetAll(context).Concat(GetBusinessRulesForGetAll(context).GetValidationResults());
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForGetAll"/> and <see cref="GetBusinessRulesForGetAllAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnGetAllCommandValidationAsync(ExecutionContext<T> context)
        {
            var rules = await GetBusinessRulesForGetAllAsync(context);
            return GetValidationResultsForGetAll(context).Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForInsert"/> and <see cref="GetBusinessRulesForInsert"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnInsertCommandValidation(T resource, ExecutionContext<T> context)
        {
            var validationErrors = GetValidationResultsForInsert(resource, context);
            var businessRuleErrors = GetBusinessRulesForInsert(resource, context).GetValidationResults();
            return validationErrors.Concat(businessRuleErrors);
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForInsert"/> and <see cref="GetBusinessRulesForInsertAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnInsertCommandValidationAsync(T resource, ExecutionContext<T> context)
        {
            var validationErrors = GetValidationResultsForInsert(resource, context);
            var rules = await GetBusinessRulesForInsertAsync(resource, context);
            return validationErrors.Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForUpdate"/> and <see cref="GetBusinessRulesForUpdate"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnUpdateCommandValidation(T resource, ExecutionContext<T> context)
        {
            var validationErrors = GetValidationResultsForUpdate(resource, context);
            var businessRuleErrors = GetBusinessRulesForUpdate(resource, context).GetValidationResults();
            return validationErrors.Concat(businessRuleErrors);
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForUpdate"/> and <see cref="GetBusinessRulesForUpdateAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnUpdateCommandValidationAsync(T resource, ExecutionContext<T> context)
        {
            var validationErrors = GetValidationResultsForUpdate(resource, context);
            var rules = await GetBusinessRulesForUpdateAsync(resource, context);
            return validationErrors.Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForDelete"/> and <see cref="GetBusinessRulesForDelete"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnDeleteCommandValidation(TKey id, ExecutionContext<T> context)
        {
            return GetValidationResultsForDelete(id, context).Concat(GetBusinessRulesForDelete(id, context).GetValidationResults());
        }

        /// <summary>
        /// Combines the results of <see cref="GetValidationResultsForDelete"/> and <see cref="GetBusinessRulesForDeleteAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list o <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules method.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnDeleteCommandValidationAsync(TKey id, ExecutionContext<T> context)
        {
            var rules = await GetBusinessRulesForDeleteAsync(id, context);
            return GetValidationResultsForDelete(id, context).Concat(await rules.GetValidationResultsAsync());
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes resource retrieval logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <returns>An invokable <see cref="ICommand{T}"/></returns>
        public virtual ICommand<T> GetByIDCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnGetByIDCommandInitialization(id, context),
                initializationAsyncMethod: () => OnGetByIDCommandInitializationAsync(id, context),
                validationMethod: () => OnGetByIDCommandValidation(id, context),
                validationAsyncMethod: () => OnGetByIDCommandValidationAsync(id, context),
                executeMethod: () => OnGetByIDCommandValidationSuccess(id, context),
                executeAsyncMethod: () => OnGetByIDCommandValidationSuccessAsync(id, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes multiple resource retrieval logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <returns>An invokable ICommand&lt;T[]&gt;</returns>
        public virtual ICommand<IEnumerable<T>> GetAllCommand()
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<IEnumerable<T>>
            (
                initializationMethod: () => OnGetAllCommandInitialization(context),
                initializationAsyncMethod: () => OnGetAllCommandInitializationAsync(context),
                validationMethod: () => OnGetAllCommandValidation(context),
                validationAsyncMethod: () => OnGetAllCommandValidationAsync(context),
                executeMethod: () => OnGetAllCommandValidationSuccess(context),
                executeAsyncMethod: () => OnGetAllCommandValidationSuccessAsync(context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes resource insertion logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <param name="resource">The resource to insert.</param>
        /// <returns>An invokable <see cref="ICommand{T}"/></returns>
        public virtual ICommand<T> InsertCommand(T resource)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnInsertCommandInitialization(resource, context),
                initializationAsyncMethod: () => OnInsertCommandInitializationAsync(resource, context),
                validationMethod: () => OnInsertCommandValidation(resource, context),
                validationAsyncMethod: () => OnInsertCommandValidationAsync(resource, context),
                executeMethod: () => OnInsertCommandValidationSuccess(resource, context),
                executeAsyncMethod: () => OnInsertCommandValidationSuccessAsync(resource, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes resource update logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <param name="resource">The resource to update.</param>
        /// <returns>An invokable <see cref="ICommand{T}"/></returns>
        public virtual ICommand<T> UpdateCommand(T resource)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnUpdateCommandInitialization(resource, context),
                initializationAsyncMethod: () => OnUpdateCommandInitializationAsync(resource, context),
                validationMethod: () => OnUpdateCommandValidation(resource, context),
                validationAsyncMethod: () => OnUpdateCommandValidationAsync(resource, context),
                executeMethod: () => OnUpdateCommandValidationSuccess(resource, context),
                executeAsyncMethod: () => OnUpdateCommandValidationSuccessAsync(resource, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes resource deletion logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <param name="id">The id of the resource to delete.</param>
        /// <returns>An invokable <see cref="ICommand"/></returns>
        public virtual ICommand DeleteCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand
            (
                initializationMethod: () => OnDeleteCommandInitialization(id, context),
                initializationAsyncMethod: () => OnDeleteCommandInitializationAsync(id, context),
                validationMethod: () => OnDeleteCommandValidation(id, context),
                validationAsyncMethod: () => OnDeleteCommandValidationAsync(id, context),
                executeMethod: () => OnDeleteCommandValidationSuccess(id, context),
                executeAsyncMethod: () => OnDeleteCommandValidationSuccessAsync(id, context)
            );
        }

    }
}
