using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Peasy.Extensions;

namespace Peasy
{
    /// <summary>
    /// Serves as the base class of all business services and represents an <see cref="ISynchronousCommand"/> factory.
    /// </summary>
    /// <typeparam name="T">Any type that implements <see cref="IDomainObject{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Any type.</typeparam>
    public abstract class SynchronousServiceBase<T, TKey> : ISynchronousService<T, TKey> where T : IDomainObject<TKey>, new()
    {
        /// <inheritdoc cref="ISynchronousDataProxy{T, TKey}"/>
        protected readonly ISynchronousDataProxy<T, TKey> _dataProxy;

        /// <inheritdoc cref="ISynchronousDataProxy{T, TKey}"/>
        protected ISynchronousDataProxy<T, TKey> DataProxy => _dataProxy;

        /// <summary>
        /// Initializes a new service instance with a required data proxy.
        /// </summary>
        /// <param name="dataProxy">The data proxy abstraction to be consumed by commands.</param>
        public SynchronousServiceBase(ISynchronousDataProxy<T, TKey> dataProxy)
        {
            _dataProxy = dataProxy;
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to perform initialization logic before rule validations are invoked by the command returned by <see cref="DeleteCommand"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        protected virtual void OnDeleteCommandInitialization(TKey id, ExecutionContext<T> context)
        {
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetByIDCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> OnGetBusinessRulesForGetByID(TKey id, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="GetAllCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetAllCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> OnGetBusinessRulesForGetAll(ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="InsertCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="InsertCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> OnGetBusinessRulesForInsert(T resource, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="UpdateCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="UpdateCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> OnGetBusinessRulesForUpdate(T resource, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="DeleteCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="DeleteCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="IRule"/></returns>
        protected virtual IEnumerable<IRule> OnGetBusinessRulesForDelete(TKey id, ExecutionContext<T> context)
        {
            return Enumerable.Empty<IRule>();
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A resource returned from <see cref="Peasy.ISupportSynchronousGetByID{T, TKey}.GetByID"/> of <see cref="DataProxy"/>.</returns>
        protected virtual T OnGetByIDCommandValidationSuccess(TKey id, ExecutionContext<T> context)
        {
            return _dataProxy.GetByID(id);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A resource list returned from <see cref="Peasy.ISupportSynchronousGetAll{T}.GetAll"/> of <see cref="DataProxy"/>.</returns>
        protected virtual IEnumerable<T> OnGetAllCommandValidationSuccess(ExecutionContext<T> context)
        {
            return _dataProxy.GetAll();
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
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
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An updated representation of the resource resulting from a call to <see cref="Peasy.ISupportSynchronousUpdate{T}.Update"/> of <see cref="DataProxy"/>.</returns>
        protected virtual T OnUpdateCommandValidationSuccess(T resource, ExecutionContext<T> context)
        {
            return _dataProxy.Update(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="SynchronousCommand.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns><see cref="void"/> resulting from a call to <see cref="Peasy.ISupportSynchronousDelete{T}.Delete"/> of <see cref="DataProxy"/>.</returns>
        protected virtual void OnDeleteCommandValidationSuccess(TKey id, ExecutionContext<T> context)
        {
            _dataProxy.Delete(id);
        }

        /// <summary>
        /// Generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnPerformGetByIDCommandValidation"/> and <see cref="OnPerformGetByIDCommandValidationAsync"/>.</para>
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
        /// <para>Invoked by <see cref="OnPerformInsertCommandValidation"/> and <see cref="OnPerformInsertCommandValidationAsync"/>.</para>
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
        /// <para>Invoked by <see cref="OnPerformUpdateCommandValidation"/> and <see cref="OnPerformUpdateCommandValidationAsync"/>.</para>
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
        /// <para>Invoked by <see cref="OnPerformDeleteCommandValidation"/> and <see cref="OnPerformDeleteCommandValidationAsync"/>.</para>
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
        /// Combines the results of <see cref="OnValidateIdForGetByID"/> and <see cref="OnGetBusinessRulesForGetByID"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnPerformGetByIDCommandValidation(TKey id, ExecutionContext<T> context)
        {
            return OnValidateIdForGetByID(id, context).Concat(OnGetBusinessRulesForGetByID(id, context).ValidateAll());
        }

        /// <summary>
        /// Invokes <see cref="OnGetBusinessRulesForGetAll"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnPerformGetAllCommandValidation(ExecutionContext<T> context)
        {
            return OnGetBusinessRulesForGetAll(context).ValidateAll();
        }

        /// <summary>
        /// Combines the results of <see cref="OnValidateObjectForInsert"/> and <see cref="OnGetBusinessRulesForInsert"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnPerformInsertCommandValidation(T resource, ExecutionContext<T> context)
        {
            var validationErrors = OnValidateObjectForInsert(resource, context);
            var businessRuleErrors = OnGetBusinessRulesForInsert(resource, context).ValidateAll();
            return validationErrors.Concat(businessRuleErrors);
        }

        /// <summary>
        /// Combines the results of <see cref="OnValidateObjectForUpdate"/> and <see cref="OnGetBusinessRulesForUpdate"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnPerformUpdateCommandValidation(T resource, ExecutionContext<T> context)
        {
            var validationErrors = OnValidateObjectForUpdate(resource, context);
            var businessRuleErrors = OnGetBusinessRulesForUpdate(resource, context).ValidateAll();
            return validationErrors.Concat(businessRuleErrors);
        }

        /// <summary>
        /// Combines the results of <see cref="OnValidateIdForDelete"/> and <see cref="OnGetBusinessRulesForDelete"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnPerformDeleteCommandValidation(TKey id, ExecutionContext<T> context)
        {
            return OnValidateIdForDelete(id, context).Concat(OnGetBusinessRulesForDelete(id, context).ValidateAll());
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes resource retrieval logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <returns>An invokable <see cref="ICommand{T}"/></returns>
        public virtual ISynchronousCommand<T> GetByIDCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand<T>
            (
                initializationMethod: () => OnGetByIDCommandInitialization(id, context),
                initializationAsyncMethod: () => OnGetByIDCommandInitializationAsync(id, context),
                validationMethod: () => OnPerformGetByIDCommandValidation(id, context),
                validationAsyncMethod: () => OnPerformGetByIDCommandValidationAsync(id, context),
                executeMethod: () => OnGetByIDCommandValidationSuccess(id, context),
                executeAsyncMethod: () => OnGetByIDCommandValidationSuccessAsync(id, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes multiple resource retrieval logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <returns>An invokable ICommand&lt;T[]&gt;</returns>
        public virtual ISynchronousCommand<IEnumerable<T>> GetAllCommand()
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand<IEnumerable<T>>
            (
                initializationMethod: () => OnGetAllCommandInitialization(context),
                initializationAsyncMethod: () => OnGetAllCommandInitializationAsync(context),
                validationMethod: () => OnPerformGetAllCommandValidation(context),
                validationAsyncMethod: () => OnPerformGetAllCommandValidationAsync(context),
                executeMethod: () => OnGetAllCommandValidationSuccess(context),
                executeAsyncMethod: () => OnGetAllCommandValidationSuccessAsync(context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes resource insertion logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <param name="resource">The resource to insert.</param>
        /// <returns>An invokable <see cref="ICommand{T}"/></returns>
        public virtual ISynchronousCommand<T> InsertCommand(T resource)
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand<T>
            (
                initializationMethod: () => OnInsertCommandInitialization(resource, context),
                initializationAsyncMethod: () => OnInsertCommandInitializationAsync(resource, context),
                validationMethod: () => OnPerformInsertCommandValidation(resource, context),
                validationAsyncMethod: () => OnPerformInsertCommandValidationAsync(resource, context),
                executeMethod: () => OnInsertCommandValidationSuccess(resource, context),
                executeAsyncMethod: () => OnInsertCommandValidationSuccessAsync(resource, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes resource update logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <param name="resource">The resource to update.</param>
        /// <returns>An invokable <see cref="ICommand{T}"/></returns>
        public virtual ISynchronousCommand<T> UpdateCommand(T resource)
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand<T>
            (
                initializationMethod: () => OnUpdateCommandInitialization(resource, context),
                initializationAsyncMethod: () => OnUpdateCommandInitializationAsync(resource, context),
                validationMethod: () => OnPerformUpdateCommandValidation(resource, context),
                validationAsyncMethod: () => OnPerformUpdateCommandValidationAsync(resource, context),
                executeMethod: () => OnUpdateCommandValidationSuccess(resource, context),
                executeAsyncMethod: () => OnUpdateCommandValidationSuccessAsync(resource, context)
            );
        }

        /// <summary>
        /// Composes a <see cref="Command"/> that invokes resource deletion logic based upon successful execution of business and validation rules.
        /// </summary>
        /// <param name="id">The id of the resource to delete.</param>
        /// <returns>An invokable <see cref="ICommand"/></returns>
        public virtual ISynchronousCommand DeleteCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand
            (
                initializationMethod: () => OnDeleteCommandInitialization(id, context),
                initializationAsyncMethod: () => OnDeleteCommandInitializationAsync(id, context),
                validationMethod: () => OnPerformDeleteCommandValidation(id, context),
                validationAsyncMethod: () => OnPerformDeleteCommandValidationAsync(id, context),
                executeMethod: () => OnDeleteCommandValidationSuccess(id, context),
                executeAsyncMethod: () => OnDeleteCommandValidationSuccessAsync(id, context)
            );
        }

    }
}
