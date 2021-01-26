using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Defines a base command responsible for the execution of a logical unit of work.
    /// </summary>
    public abstract class SynchronousCommand : ISynchronousCommand, ISynchronousRulesContainer, ISupportSynchronousValidation
    {
        /// <inheritdoc cref="ISynchronousCommand.Execute"/>
        public virtual ExecutionResult Execute()
        {
            OnInitialization();

            var errors = OnValidate().ToArray();

            if (errors.Any()) return OnFailedExecution(errors);

            try
            {
                OnExecute();
            }
            catch (PeasyException ex)
            {
                return OnPeasyExceptionHandled(ex);
            }

            return OnSuccessfulExecution();
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>Invoked within the execution pipeline triggered by <see cref="Execute"/>.</para>
        /// <para>Override this method to perform initialization logic before rule executions occur.</para>
        /// </remarks>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Performs rule validations.
        /// </summary>
        /// <remarks>
        /// <para>Invoked within the execution pipeline triggered by <see cref="Execute"/>.</para>
        /// <para>Override this method to manipulate how rules are invoked.</para>
        /// </remarks>
        /// <returns>A potential list of errors resulting from rule executions.</returns>
        protected virtual IEnumerable<ValidationResult> OnValidate()
        {
            return OnGetRules().ValidateAll();
        }

        /// <summary>
        /// Executes application logic.
        /// </summary>
        /// <remarks>
        /// <para>Invoked within the execution pipeline triggered by <see cref="Execute"/> if all rule validations are successful.</para>
        /// <para>Override this method to perform custom application logic.</para>
        /// </remarks>
        protected virtual void OnExecute() { }

        /// <summary>
        /// Composes a list of business and validation rules to execute.
        /// </summary>
        /// <remarks>
        /// <para>Invoked within the execution pipeline triggered by <see cref="Execute"/>.</para>
        /// <para>Override this method to supply custom business rules to execute.</para>
        /// </remarks>
        /// <returns>A list of business and validation rules.</returns>
        protected virtual IEnumerable<ISynchronousRule> OnGetRules()
        {
            return Enumerable.Empty<ISynchronousRule>();
        }

        /// <summary>
        /// Invoked when an exception of type <see cref="PeasyException"/> is handled.
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom execution result of type <see cref="ExecutionResult"/> or to further manipulate it when a <see cref="PeasyException"/> is handled.
        /// </remarks>
        /// <returns>A failed execution result.</returns>
        protected virtual ExecutionResult OnPeasyExceptionHandled(PeasyException exception)
        {
            return OnFailedExecution(new[] { new ValidationResult(exception.Message) });
        }

        /// <summary>
        /// Invoked when any of the rules in the pipeline fail execution.
        /// </summary>
        /// <remarks>
        /// <para>Override this method to return a custom execution result of type <see cref="ExecutionResult"/> or to further manipulate it.</para>
        /// <para>Override this method to return custom validation results of type <see cref="ValidationResult"/> or to further manipulate them.</para>
        /// </remarks>
        /// <returns>A failed execution result.</returns>
        protected virtual ExecutionResult OnFailedExecution(IEnumerable<ValidationResult> validationResults)
        {
            return new ExecutionResult { Success = false, Errors = validationResults };
        }

        /// <summary>
        /// Invoked when the successful execution of the command pipeline is complete.
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom execution result of type <see cref="ExecutionResult"/> or to further manipulate it.
        /// </remarks>
        /// <returns>A successful execution result.</returns>
        protected virtual ExecutionResult OnSuccessfulExecution()
        {
            return new ExecutionResult { Success = true };
        }

        /// <inheritdoc cref="ISynchronousRulesContainer.GetRules"/>
        public IEnumerable<ISynchronousRule> GetRules()
        {
            return OnGetRules();
        }

        /// <inheritdoc cref="ISupportSynchronousValidation.Validate"/>
        public IEnumerable<ValidationResult> Validate()
        {
            return OnValidate();
        }
    }

    /// <summary>
    /// Defines a base command responsible for the execution of a logical unit of work.
    /// </summary>
    public abstract class SynchronousCommand<T> : ISynchronousCommand<T>, ISynchronousRulesContainer, ISupportSynchronousValidation
    {
        /// <inheritdoc cref="ISynchronousCommand{T}.Execute"/>
        public virtual ExecutionResult<T> Execute()
        {
            OnInitialization();

            var validationResults = OnValidate().ToArray();

            if (validationResults.Any()) return OnFailedExecution(validationResults);

            T result;
            try
            {
                result = OnExecute();
            }
            catch (PeasyException ex)
            {
                return OnPeasyExceptionHandled(ex);
            }

            return OnSuccessfulExecution(result);
        }

        /// <summary>
        /// Performs initialization logic.
        /// </summary>
        /// <remarks>
        /// <para>Invoked within the execution pipeline triggered by <see cref="Execute"/>.</para>
        /// <para>Override this method to perform initialization logic before rule executions occur.</para>
        /// </remarks>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Performs rule validations.
        /// </summary>
        /// <remarks>
        /// <para>Invoked within the execution pipeline triggered by <see cref="Execute"/>.</para>
        /// <para>Override this method to manipulate how rules are invoked.</para>
        /// </remarks>
        /// <returns>A potential list of errors resulting from rule executions.</returns>
        protected virtual IEnumerable<ValidationResult> OnValidate()
        {
            return OnGetRules().ValidateAll();
        }

        /// <summary>
        /// Executes application logic.
        /// </summary>
        /// <remarks>
        /// <para>Invoked within the execution pipeline triggered by <see cref="Execute"/> if all rule validations are successful.</para>
        /// <para>Override this method to perform custom application logic.</para>
        /// </remarks>
        /// <returns>A resource of type <typeparamref name="T"/> resulting from successful command execution.</returns>
        protected virtual T OnExecute()
        {
            return default(T);
        }

        /// <summary>
        /// Composes a list of business and validation rules to execute.
        /// </summary>
        /// <remarks>
        /// <para>Invoked within the execution pipeline triggered by <see cref="Execute"/>.</para>
        /// <para>Override this method to supply custom business rules to execute.</para>
        /// </remarks>
        /// <returns>A list of business and validation rules.</returns>
        protected virtual IEnumerable<ISynchronousRule> OnGetRules()
        {
            return Enumerable.Empty<ISynchronousRule>();
        }

        /// <summary>
        /// Invoked when an exception of type <see cref="PeasyException"/> is handled.
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom execution result of type <see cref="ExecutionResult{T}"/> or to further manipulate it when a <see cref="PeasyException"/> is handled.
        /// </remarks>
        /// <returns>A failed execution result.</returns>
        protected virtual ExecutionResult<T> OnPeasyExceptionHandled(PeasyException exception)
        {
            return OnFailedExecution(new[] { new ValidationResult(exception.Message) });
        }

        /// <summary>
        /// Invoked when any of the rules in the pipeline fail execution.
        /// </summary>
        /// <remarks>
        /// <para>Override this method to return a custom execution result of type <see cref="ExecutionResult{T}"/> or to further manipulate it.</para>
        /// <para>Override this method to return custom validation results of type <see cref="ValidationResult"/> or to further manipulate them.</para>
        /// </remarks>
        /// <returns>A failed execution result.</returns>
        protected virtual ExecutionResult<T> OnFailedExecution(ValidationResult[] validationResults)
        {
            return new ExecutionResult<T> { Success = false, Errors = validationResults };
        }

        /// <summary>
        /// Invoked when the successful execution of the command pipeline is complete.
        /// </summary>
        /// <remarks>
        /// Override this method to return a custom execution result of type <see cref="ExecutionResult{T}"/> or to further manipulate it.
        /// </remarks>
        /// <returns>A successful execution result.</returns>
        protected virtual ExecutionResult<T> OnSuccessfulExecution(T value)
        {
            return new ExecutionResult<T> { Success = true, Value = value };
        }

        /// <inheritdoc cref="ISynchronousRulesContainer.GetRules"/>
        public IEnumerable<ISynchronousRule> GetRules()
        {
            return OnGetRules();
        }

        /// <inheritdoc cref="ISupportSynchronousValidation.Validate"/>
        public IEnumerable<ValidationResult> Validate()
        {
            return OnValidate();
        }
    }
}
