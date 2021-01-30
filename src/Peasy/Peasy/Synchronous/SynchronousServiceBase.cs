using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Peasy.Extensions;

namespace Peasy.Synchronous
{
    /// <summary>
    /// Serves as the base class for business services and represents an <see cref="ISynchronousCommand"/> factory.
    /// </summary>
    /// <typeparam name="T">Represents a domain object or resource and can be any type that implements <see cref="IDomainObject{TKey}"/>.</typeparam>
    /// <typeparam name="TKey">Represents an identifier for a domain object or resource and can be any type.</typeparam>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
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
        /// <para>The first method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
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
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetByIDCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="ISynchronousRule"/>.</returns>
        protected virtual IEnumerable<ISynchronousRule> OnGetByIDCommandGetRules(TKey id, ExecutionContext<T> context)
        {
            return Enumerable.Empty<ISynchronousRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="GetAllCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="GetAllCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="ISynchronousRule"/>.</returns>
        protected virtual IEnumerable<ISynchronousRule> OnGetAllCommandGetRules(ExecutionContext<T> context)
        {
            return Enumerable.Empty<ISynchronousRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="InsertCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="InsertCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="ISynchronousRule"/>.</returns>
        protected virtual IEnumerable<ISynchronousRule> OnInsertCommandGetRules(T resource, ExecutionContext<T> context)
        {
            return Enumerable.Empty<ISynchronousRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="UpdateCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="UpdateCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="ISynchronousRule"/>.</returns>
        protected virtual IEnumerable<ISynchronousRule> OnUpdateCommandGetRules(T resource, ExecutionContext<T> context)
        {
            return Enumerable.Empty<ISynchronousRule>();
        }

        /// <summary>
        /// Returns a list of business rules to be executed within the execution pipeline of the command returned by <see cref="DeleteCommand"/>.
        /// </summary>
        /// <remarks>
        /// <para>This is the second method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to supply business rules for invocation by the command returned by <see cref="DeleteCommand"/>.</para>
        /// <para>The successful invocation of the returned rules will determine whether or not to proceed with command pipeline execution.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="ISynchronousRule"/>.</returns>
        protected virtual IEnumerable<ISynchronousRule> OnDeleteCommandGetRules(TKey id, ExecutionContext<T> context)
        {
            return Enumerable.Empty<ISynchronousRule>();
        }

        /// <summary>
        /// Combines the results of <see cref="OnGetByIDCommandValidateID"/> and <see cref="OnGetByIDCommandGetRules"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/>.</returns>
        protected virtual IEnumerable<ValidationResult> OnGetByIDCommandPerformValidation(TKey id, ExecutionContext<T> context)
        {
            var validationErrors = OnGetByIDCommandValidateID(id, context);
            var rules = OnGetByIDCommandGetRules(id, context);
            return validationErrors.Concat(rules.ValidateAll());
        }

        /// <summary>
        /// Invokes <see cref="OnGetAllCommandGetRules"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/>.</returns>
        protected virtual IEnumerable<ValidationResult> OnGetAllCommandPerformValidation(ExecutionContext<T> context)
        {
            var rules = OnGetAllCommandGetRules(context);
            return rules.ValidateAll();
        }

        /// <summary>
        /// Combines the results of <see cref="OnInsertCommandValidateObject"/> and <see cref="OnInsertCommandGetRules"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/>.</returns>
        protected virtual IEnumerable<ValidationResult> OnInsertCommandPerformValidation(T resource, ExecutionContext<T> context)
        {
            var validationErrors = OnInsertCommandValidateObject(resource, context);
            var rules = OnInsertCommandGetRules(resource, context);
            return validationErrors.Concat(rules.ValidateAll());
        }

        /// <summary>
        /// Combines the results of <see cref="OnUpdateCommandValidateObject"/> and <see cref="OnUpdateCommandGetRules"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/>.</returns>
        protected virtual IEnumerable<ValidationResult> OnUpdateCommandPerformValidation(T resource, ExecutionContext<T> context)
        {
            var validationErrors = OnUpdateCommandValidateObject(resource, context);
            var rules = OnUpdateCommandGetRules(resource, context);
            return validationErrors.Concat(rules.ValidateAll());
        }

        /// <summary>
        /// Combines the results of <see cref="OnDeleteCommandValidateId"/> and <see cref="OnDeleteCommandGetRules"/> to generate potential list of <see cref="ValidationResult"/>.
        /// </summary>
        /// <remarks>Override this method to manipulate the creation of a list o <see cref="ValidationResult"/>.
        /// <para>Example: You might want to verify that no validation errors exist before invoking the potentially expensive business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/>.</returns>
        protected virtual IEnumerable<ValidationResult> OnDeleteCommandPerformValidation(TKey id, ExecutionContext<T> context)
        {
            var validationErrors = OnDeleteCommandValidateId(id, context);
            var rules = OnDeleteCommandGetRules(id, context);
            return validationErrors.Concat(rules.ValidateAll());
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetByIDCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A resource returned from <see cref="ISupportSynchronousGetByID{T, TKey}.GetByID"/> of <see cref="DataProxy"/>.</returns>
        protected virtual T OnGetByIDCommandValidationSuccess(TKey id, ExecutionContext<T> context)
        {
            return _dataProxy.GetByID(id);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="GetAllCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetAllCommand"/>.</param>
        /// <returns>A resource list returned from <see cref="ISupportSynchronousGetAll{T}.GetAll"/> of <see cref="DataProxy"/>.</returns>
        protected virtual IEnumerable<T> OnGetAllCommandValidationSuccess(ExecutionContext<T> context)
        {
            return _dataProxy.GetAll();
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="InsertCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>An updated representation of the resource resulting from a call to <see cref="ISupportSynchronousInsert{T}.Insert"/> of <see cref="DataProxy"/>.</returns>
        protected virtual T OnInsertCommandValidationSuccess(T resource, ExecutionContext<T> context)
        {
            return _dataProxy.Insert(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="UpdateCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>An updated representation of the resource resulting from a call to <see cref="ISupportSynchronousUpdate{T}.Update"/> of <see cref="DataProxy"/>.</returns>
        protected virtual T OnUpdateCommandValidationSuccess(T resource, ExecutionContext<T> context)
        {
            return _dataProxy.Update(resource);
        }

        /// <summary>
        /// Performs additional business logic and data proxy interaction.
        /// </summary>
        /// <remarks>
        /// <para>This is the third and final method invoked within the execution pipeline of the command returned by <see cref="DeleteCommand"/>, triggered by <see cref="SynchronousCommandBase.Execute"/>.</para>
        /// <para>Override this method to invoke custom business logic and data proxy interaction.</para>
        /// <para>This method is only invoked based on the successful validation of all configured validation and business rules.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        protected virtual void OnDeleteCommandValidationSuccess(TKey id, ExecutionContext<T> context)
        {
            _dataProxy.Delete(id);
        }

        /// <summary>
        /// Generates a potential list of <see cref="ValidationResult"/> based on the supplied resource.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnGetByIDCommandPerformValidation"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="GetByIDCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnGetByIDCommandValidateID(TKey id, ExecutionContext<T> context)
        {
            yield break;
        }

        /// <summary>
        /// Performs validation against the supplied resource by validating property values applied with attributes of type <see cref="ValidationAttribute"/>.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnInsertCommandPerformValidation"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to insert.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="InsertCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnInsertCommandValidateObject(T resource, ExecutionContext<T> context)
        {
            return resource.Validate();
        }

        /// <summary>
        /// Performs validation against the supplied resource by validating property values applied with attributes of type <see cref="ValidationAttribute"/>.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnUpdateCommandPerformValidation"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="resource">The resource to update.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="UpdateCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnUpdateCommandValidateObject(T resource, ExecutionContext<T> context)
        {
            return resource.Validate();
        }

        /// <summary>
        /// Performs validation against the supplied id.
        /// </summary>
        /// <remarks>
        /// <para>Invoked by <see cref="OnDeleteCommandPerformValidation"/>.</para>
        /// <para>Override this method to manipulate the creation of a list of <see cref="ValidationResult"/>.</para>
        /// </remarks>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="context">Serves as shared state between all pipeline methods invoked by the command returned by <see cref="DeleteCommand"/>.</param>
        /// <returns>A list of <see cref="ValidationResult"/></returns>
        protected virtual IEnumerable<ValidationResult> OnDeleteCommandValidateId(TKey id, ExecutionContext<T> context)
        {
            yield break;
        }

        /// <inheritdoc cref="ISupportSynchronousGetByIDCommand{T, TKey}.GetByIDCommand"/>
        public virtual ISynchronousCommand<T> GetByIDCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand<T>
            (
                initializationMethod: () => OnGetByIDCommandInitialization(id, context),
                validationMethod: () => OnGetByIDCommandPerformValidation(id, context),
                executeMethod: () => OnGetByIDCommandValidationSuccess(id, context)
            );
        }

        /// <inheritdoc cref="ISupportSynchronousGetAllCommand{T}.GetAllCommand"/>
        public virtual ISynchronousCommand<IEnumerable<T>> GetAllCommand()
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand<IEnumerable<T>>
            (
                initializationMethod: () => OnGetAllCommandInitialization(context),
                validationMethod: () => OnGetAllCommandPerformValidation(context),
                executeMethod: () => OnGetAllCommandValidationSuccess(context)
            );
        }

        /// <inheritdoc cref="ISupportSynchronousInsertCommand{T}.InsertCommand"/>
        public virtual ISynchronousCommand<T> InsertCommand(T resource)
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand<T>
            (
                initializationMethod: () => OnInsertCommandInitialization(resource, context),
                validationMethod: () => OnInsertCommandPerformValidation(resource, context),
                executeMethod: () => OnInsertCommandValidationSuccess(resource, context)
            );
        }

        /// <inheritdoc cref="ISupportSynchronousUpdateCommand{T}.UpdateCommand"/>
        public virtual ISynchronousCommand<T> UpdateCommand(T resource)
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand<T>
            (
                initializationMethod: () => OnUpdateCommandInitialization(resource, context),
                validationMethod: () => OnUpdateCommandPerformValidation(resource, context),
                executeMethod: () => OnUpdateCommandValidationSuccess(resource, context)
            );
        }

        /// <inheritdoc cref="ISupportSynchronousDeleteCommand{TKey}.DeleteCommand"/>
        public virtual ISynchronousCommand DeleteCommand(TKey id)
        {
            var context = new ExecutionContext<T>();
            return new SynchronousServiceCommand
            (
                initializationMethod: () => OnDeleteCommandInitialization(id, context),
                validationMethod: () => OnDeleteCommandPerformValidation(id, context),
                executeMethod: () => OnDeleteCommandValidationSuccess(id, context)
            );
        }
    }
}
