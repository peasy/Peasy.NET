using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peasy
{
    /// <inheritdoc cref="IRule"/>
    public abstract class RuleBase : IRule, IRuleSuccessorsContainer<IRule>
    {
        /// <summary>
        /// An action to perform if this rule passes validation.
        /// </summary>
        protected Action<IRule> _ifValidThenInvoke;

        /// <summary>
        /// An action to perform if this rule fails validation.
        /// </summary>
        protected Action<IRule> _ifInvalidThenInvoke;

        /// <inheritdoc cref="IRule.Association"/>
        public string Association { get; protected set; }

        /// <inheritdoc cref="IRule.ErrorMessage"/>
        public string ErrorMessage { get; protected set; }

        /// <inheritdoc cref="IRule.IsValid"/>
        public bool IsValid { get; protected set; }

        /// <summary>
        /// Gets or sets the list of <see cref="IRule"/> that should be evaluated upon successful validation.
        /// </summary>
        private List<IRuleSuccessor<IRule>> Successors { set; get; } = new List<IRuleSuccessor<IRule>>();

        /// <inheritdoc cref="IRule.ExecuteAsync"/>
        public async Task<IRule> ExecuteAsync()
        {
            IsValid = true;
            await OnValidateAsync();
            if (IsValid)
            {
                if (Successors != null)
                {
                    foreach (var successor in Successors)
                    {
                        foreach (var rule in successor)
                        {
                            await rule.ExecuteAsync();
                            if (!rule.IsValid)
                            {
                                Invalidate(rule.ErrorMessage, rule.Association);
                                _ifInvalidThenInvoke?.Invoke(this);
                                break; // early exit, don't bother further rule execution
                            }
                        }
                        if (!IsValid) break;
                    }
                }
                _ifValidThenInvoke?.Invoke(this);
            }
            else
            {
                _ifInvalidThenInvoke?.Invoke(this);
            }
            return this;
        }

        /// <summary>
        /// Validates the supplied list of rules upon successful validation of this rule.
        /// </summary>
        /// <param name="rules">The rules to validate.</param>
        /// <returns>A reference to this rule.</returns>
        public RuleBase IfValidThenValidate(params IRule[] rules)
        {
            Successors.Add(new RuleSuccessor<IRule>(rules));
            return this;
        }

        /// <summary>
        /// Executes the supplied action upon successful validation of this rule.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        /// <returns>A reference to this rule.</returns>
        public RuleBase IfValidThenInvoke(Action<IRule> method)
        {
            _ifValidThenInvoke = method;
            return this;
        }

        /// <summary>
        /// Executes the supplied action upon failed validation of this rule.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        /// <returns>A reference to this rule.</returns>
        public RuleBase IfInvalidThenInvoke(Action<IRule> method)
        {
            _ifInvalidThenInvoke = method;
            return this;
        }

        /// <summary>
        /// Performs business or validation logic.
        /// </summary>
        /// <remarks>
        /// <para>This method is called upon invocation of <see cref="RuleBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to perform custom business or validation logic.</para>
        /// </remarks>
        /// <returns>An awaitable task.</returns>
        protected virtual Task OnValidateAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Invalidates this rule.
        /// </summary>
        /// <remarks>Invoke this method to invalidate this rule.</remarks>
        /// <param name="errorMessage">Sets the <see cref="ErrorMessage"/> value.</param>
        protected virtual void Invalidate(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsValid = false;
        }

        /// <summary>
        /// Invalidates this rule.
        /// </summary>
        /// <remarks>Invoke this method to invalidate this rule.</remarks>
        /// <param name="errorMessage">Sets the <see cref="ErrorMessage"/> value.</param>
        /// <param name="association">Sets the <see cref="Association"/> value.</param>
        protected virtual void Invalidate(string errorMessage, string association)
        {
            Association = association;
            Invalidate(errorMessage);
        }

        ///<inheritdoc cref="IRuleSuccessorsContainer{T}.GetSuccessors"/>
        public IEnumerable<IRuleSuccessor<IRule>> GetSuccessors()
        {
            return Successors;
        }
    }
}
