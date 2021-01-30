using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Peasy.Extensions;

namespace Peasy
{
    /// <summary>
    /// Serves as the base class for business services and represents an <see cref="ICommand"/> factory.
    /// </summary>
    /// <typeparam name="T">Represents a domain object or resource and can be any type that implements <see cref="IDomainObject{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Represents an identifier for a domain object or resource and can be any type.</typeparam>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
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
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetByIDCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> OnGetBusinessRulesForGetByIDAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="GetAllCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetAllCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> OnGetBusinessRulesForGetAllAsync(ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="InsertCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="InsertCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> OnGetBusinessRulesForInsertAsync(T resource, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="UpdateCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="UpdateCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> OnGetBusinessRulesForUpdateAsync(T resource, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="DeleteCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="DeleteCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="IRule"/></returns>
        protected virtual Task<IEnumerable<IRule>> OnGetBusinessRulesForDeleteAsync(TKey id, ExecutionContext<T> context)
        {
            return Task.FromResult(Enumerable.Empty<IRule>());
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>An awaitable resource returned from <see cref="Peasy.ISupportGetByID{T, TKey}.GetByIDAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual async Task<T> OnGetByIDCommandValidationSuccessAsync(TKey id, ExecutionContext<T> context)
        {
            return await _dataProxy.GetByIDAsync(id);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>An awaitable resource list returned from <see cref="Peasy.ISupportGetAll{T}.GetAllAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual async Task<IEnumerable<T>> OnGetAllCommandValidationSuccessAsync(ExecutionContext<T> context)
        {
            return await _dataProxy.GetAllAsync();
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An awaitable updated representation of the resource resulting from a call to <see cref="Peasy.ISupportInsert{T}.InsertAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual async Task<T> OnInsertCommandValidationSuccessAsync(T resource, ExecutionContext<T> context)
        {
            return await _dataProxy.InsertAsync(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An awaitable updated representation of the resource resulting from a call to <see cref="Peasy.ISupportUpdate{T}.UpdateAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual async Task<T> OnUpdateCommandValidationSuccessAsync(T resource, ExecutionContext<T> context)
        {
            return await _dataProxy.UpdateAsync(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="CommandBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>An awaitable <see cref="Task"/> resulting from a call to <see cref="Peasy.ISupportDelete{T}.DeleteAsync"/> of <see cref="DataProxy"/>.</returns>
        protected virtual Task OnDeleteCommandValidationSuccessAsync(TKey id, ExecutionContext<T> context)
        {
            return _dataProxy.DeleteAsync(id);
        }

        /// <summary>
        /// Generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnPerformGetByIDCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnValidateIdForGetByID(TKey id, ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Performs validation against the supplied resource by validating property values applied with attributes of type <see cref="ValidationAttribute"/>.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnPerformInsertCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnValidateObjectForInsert(T resource, ExecutionContext<T> context)
        {
            return resource.Validate();
        }

        /// <summary>
        /// Performs validation against the supplied resource by validating property values applied with attributes of type <see cref="ValidationAttribute"/>.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnPerformUpdateCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnValidateObjectForUpdate(T resource, ExecutionContext<T> context)
        {
            return resource.Validate();
        }

        /// <summary>
        /// Performs validation against the supplied id.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnPerformDeleteCommandValidationAsync"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnValidateIdForDelete(TKey id, ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Combines the results of <see cref="OnValidateIdForGetByID"/> and <see cref="OnGetBusinessRulesForGetByIDAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnPerformGetByIDCommandValidationAsync(TKey id, ExecutionContext<T> context)
        {
            var validationErrors = OnValidateIdForGetByID(id, context);
            var rules = await OnGetBusinessRulesForGetByIDAsync(id, context);
            return validationErrors.Concat(await rules.ValidateAllAsync());
        }

        /// <summary>
        /// Invokes <see cref="OnGetBusinessRulesForGetAllAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnPerformGetAllCommandValidationAsync(ExecutionContext<T> context)
        {
            var rules = await OnGetBusinessRulesForGetAllAsync(context);
            return await rules.ValidateAllAsync();
        }

        /// <summary>
        /// Combines the results of <see cref="OnValidateObjectForInsert"/> and <see cref="OnGetBusinessRulesForInsertAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnPerformInsertCommandValidationAsync(T resource, ExecutionContext<T> context)
        {
            var validationErrors = OnValidateObjectForInsert(resource, context);
            var rules = await OnGetBusinessRulesForInsertAsync(resource, context);
            return validationErrors.Concat(await rules.ValidateAllAsync());
        }

        /// <summary>
        /// Combines the results of <see cref="OnValidateObjectForUpdate"/> and <see cref="OnGetBusinessRulesForUpdateAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnPerformUpdateCommandValidationAsync(T resource, ExecutionContext<T> context)
        {
            var validationErrors = OnValidateObjectForUpdate(resource, context);
            var rules = await OnGetBusinessRulesForUpdateAsync(resource, context);
            return validationErrors.Concat(await rules.ValidateAllAsync());
        }

        /// <summary>
        /// Combines the results of <see cref="OnValidateIdForDelete"/> and <see cref="OnGetBusinessRulesForDeleteAsync"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list o <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>An awaitable list of <see cref="ValidationResult"/></returns>
        protected virtual async Task<IEnumerable<ValidationResult>> OnPerformDeleteCommandValidationAsync(TKey id, ExecutionContext<T> context)
        {
            var validationErrors = OnValidateIdForDelete(id, context);
            var rules = await OnGetBusinessRulesForDeleteAsync(id, context);
            return validationErrors.Concat(await rules.ValidateAllAsync());
        }

        /// <inheritdoc cref="ISupportGetByIDCommand{T, TKey}.GetByIDCommand"/>
        public virtual ICommand<T> GetByIDCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnGetByIDCommandInitializationAsync(id, context),
                validationMethod: () => OnPerformGetByIDCommandValidationAsync(id, context),
                executeMethod: () => OnGetByIDCommandValidationSuccessAsync(id, context)
            );
        }

        /// <inheritdoc cref="ISupportGetAllCommand{T}.GetAllCommand"/>
        public virtual ICommand<IEnumerable<T>> GetAllCommand()
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<IEnumerable<T>>
            (
                initializationMethod: () => OnGetAllCommandInitializationAsync(context),
                validationMethod: () => OnPerformGetAllCommandValidationAsync(context),
                executeMethod: () => OnGetAllCommandValidationSuccessAsync(context)
            );
        }

        /// <inheritdoc cref="ISupportInsertCommand{T}.InsertCommand"/>
        public virtual ICommand<T> InsertCommand(T resource)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnInsertCommandInitializationAsync(resource, context),
                validationMethod: () => OnPerformInsertCommandValidationAsync(resource, context),
                executeMethod: () => OnInsertCommandValidationSuccessAsync(resource, context)
            );
        }

        /// <inheritdoc cref="ISupportUpdateCommand{T}.UpdateCommand"/>
        public virtual ICommand<T> UpdateCommand(T resource)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand<T>
            (
                initializationMethod: () => OnUpdateCommandInitializationAsync(resource, context),
                validationMethod: () => OnPerformUpdateCommandValidationAsync(resource, context),
                executeMethod: () => OnUpdateCommandValidationSuccessAsync(resource, context)
            );
        }

        /// <inheritdoc cref="ISupportDeleteCommand{TKey}.DeleteCommand"/>
        public virtual ICommand DeleteCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new ServiceCommand
            (
                initializationMethod: () => OnDeleteCommandInitializationAsync(id, context),
                validationMethod: () => OnPerformDeleteCommandValidationAsync(id, context),
                executeMethod: () => OnDeleteCommandValidationSuccessAsync(id, context)
            );
        }
    }
}
